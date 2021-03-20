using RTP.Net.RTCP;
using System;
using System.Collections.Generic;
namespace RTP.Net
{
    internal class Session
    {
        Dictionary<uint, Source> sourceTable;

        /// <summary>
        ///     The average compound RTCP packet size, in octets,
        ///     over all RTCP packets sent and received by this participant.The
        ///     size includes lower-layer transport and network protocol headers
        ///     (e.g., UDP and IP) as explained in Section 6.2.
        /// </summary>
        private uint _avg_rtcp_size;

        /// <summary>
        ///     The current time.
        /// </summary>
        private DateTime _currentTime;

        /// <summary>
        ///     initial: Flag that is true if the application has not yet sent
        ///     an RTCP packet.
        /// </summary>
        private bool _initial;

        /// <summary>
        ///     The most current estimate for the number of session members.
        /// </summary>
        private int _members;

        /// <summary>
        ///     The estimated number of session members at the time the
        ///     next scheduled transmission time was last recomputed.
        /// </summary>
        private uint _prevMembers;

        /// <summary>
        ///     The target RTCP bandwidth, i.e., the total bandwidth
        ///     that will be used for RTCP packets by all members of this session,
        ///     in octets per second.This will be a specified fraction of the
        ///     "session bandwidth" parameter supplied to the application at
        ///     startup.
        /// </summary>
        private double _rtcp_bw;

        /// <summary>
        ///     The most current estimate for the number of senders in the session.
        /// </summary>
        private int _senders;

        /// <summary>
        ///     The next scheduled transmission time of an RTCP packet.
        /// </summary>
        private int _tn;

        /// <summary>
        ///     The last time an RTCP packet was transmitted.
        /// </summary>
        private int _tp;

        /// <summary>
        ///     Our calculated interval.
        /// </summary>
        private double _calculatedInterval;

        /// <summary>
        ///     Flag that is true if and only if the application has sent data since
        ///     the second previous RTCP report was transmitted.
        /// </summary>
        private bool _we_sent;

        public Session(uint avgRtcpSize)
        {
            this.sourceTable = new Dictionary<uint, Source>();

            _avg_rtcp_size = avgRtcpSize;
            this._tp = 0;
            this._currentTime = DateTime.Now;
            this._senders = 0;
            this._prevMembers = 1;
            this._members = 1;
            this._we_sent = false;
            this._initial = true;
            this._calculatedInterval = this.CalculateTransmissionInterval();
        }


        /// <summary>
        ///     Calculates the transmission interval of our packets.
        ///     Source: (https://tools.ietf.org/html/rfc3550#section-6.3)
        /// </summary>
        private double CalculateTransmissionInterval()
        {
            if (_rtcp_bw == 0) throw new DivideByZeroException();

            /*
            * Minimum average time between RTCP packets from this site (in
            * seconds).  This time prevents the reports from `clumping' when
            * sessions are small and the law of large numbers isn't helping
            * to smooth out the traffic.  It also keeps the report interval
            * from becoming ridiculously small during transient outages like
            * a network partition.
            */
            var RTCP_MIN_TIME = (this._initial) ? 2.5 : 5;

            // current bandwidth
            double current_bandwidth = this._rtcp_bw;

            // our mutable "constant" n
            int n; /* no. of members for computation */
            double t; /* interval */
            /*
             * Fraction of the RTCP bandwidth to be shared among active
             * senders.  (This fraction was chosen so that in a typical
             * session with one or two active senders, the computed report
             * time would be roughly equal to the minimum report time so that
             * we don't unnecessarily slow down receiver reports.)  The
             * receiver fraction must be 1 - the sender fraction.
             */
            const double RTCP_SENDER_BW_FRACTION = 0.25;
            const double RTCP_RCVR_BW_FRACTION = (1 - RTCP_SENDER_BW_FRACTION);

            /*
             * To compensate for "timer reconsideration" converging to a
             * value below the intended average.
             */
            const double COMPENSATION = Math.E - ((double)3 / 2);
            /*
             * Dedicate a fraction of the RTCP bandwidth to senders unless
             * the number of senders is large enough that their share is
             * more than that fraction.
             */
            n = _members;
            if (this._senders <= RTCP_RCVR_BW_FRACTION * this._members)
            {
                // Checks whether or not we sent the packet
                if (this._we_sent)
                {
                    current_bandwidth = this._avg_rtcp_size / (RTCP_SENDER_BW_FRACTION * this._rtcp_bw);
                    n = this._senders;
                } 
                else
                {
                    current_bandwidth = _avg_rtcp_size / (RTCP_RCVR_BW_FRACTION * this._rtcp_bw);
                    n -= this._senders;
                }
            }

            /*
            * The effective number of sites times the average packet size is
            * the total number of octets sent when each site sends a report.
            * Dividing this by the effective bandwidth gives the time
            * interval over which those packets must be sent in order to
            * meet the bandwidth target, with a minimum enforced.  In that
            * time interval we send one report so this time is also our
            * average time between reports.
            */

            t = _avg_rtcp_size * n / current_bandwidth;
            if (t < RTCP_MIN_TIME) t = RTCP_MIN_TIME;
            // calculates our interval Td
            //var deterministicCalculatedInterval = Math.Max(RTCP_MIN_TIME, n*current_bandwidth);

            // initializes a new random
            var random = new Random();

            /*
             * To avoid traffic bursts from unintended synchronization with
             * other sites, we then pick our actual next report interval as a
             * random number uniformly distributed between 0.5*t and 1.5*t.
             */
            t = t * random.NextDouble();
            t /= COMPENSATION;

            return t;
        }
    }
}