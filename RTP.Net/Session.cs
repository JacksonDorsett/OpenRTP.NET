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
        private uint _members;

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
            this.CalculateTransmissionInterval();
        }

        private void UpdateSource(RTCP_RR_Block RR)
        {
            Source s = this.sourceTable[RR.SSRC];

            
        }
        
        //private double RTCP_Interval
        //{
        //    get
        //    {
        //        /*
        //        * Minimum average time between RTCP packets from this site (in
        //        * seconds).  This time prevents the reports from `clumping' when
        //        * sessions are small and the law of large numbers isn't helping
        //        * to smooth out the traffic.  It also keeps the report interval
        //        * from becoming ridiculously small during transient outages like
        //        * a network partition.
        //        */
        //        const double RTCP_MIN_TIME = 5d;

        //        /*
        //        * Fraction of the RTCP bandwidth to be shared among active
        //        * senders.  (This fraction was chosen so that in a typical
        //        * session with one or two active senders, the computed report
        //        * time would be roughly equal to the minimum report time so that
        //        * we don't unnecessarily slow down receiver reports.)  The
        //        * receiver fraction must be 1 - the sender fraction.
        //        */
        //        const double RTCP_SENDER_BW_FRACTION = 0.25;
        //        const double RTCP_RCVR_BW_FRACTION = (1 - RTCP_SENDER_BW_FRACTION);
        //        /*
        //       /* To compensate for "timer reconsideration" converging to a
        //        * value below the intended average.
        //        */
        //        const double COMPENSATION = 2.71828 - 1.5;
        //        double t;                   /* interval */
        //        double rtcp_min_time = RTCP_MIN_TIME;
        //        int n;                      /* no. of members for computation */

        //        if (_initial) rtcp_min_time /= 2;
        //        /*
        //        * Dedicate a fraction of the RTCP bandwidth to senders unless
        //        * the number of senders is large enough that their share is
        //        * more than that fraction.
        //        */
        //        n = (int)_members;

        //        if(_senders <= _members * RTCP_SENDER_BW_FRACTION)
        //        {
        //            if (_we_sent)
        //            {
        //                _rtcp_bw *= RTCP_SENDER_BW_FRACTION;
        //                n = _senders;
        //            }
        //            else
        //            {
        //                _rtcp_bw *= RTCP_RCVR_BW_FRACTION;
        //                n -= _senders;
        //            }
        //        }
        //    }
        //}
        //*/
        /// <summary>
        ///     Calculates the transmission interval of our packets.
        ///     Source: (https://tools.ietf.org/html/rfc3550#section-6.3)
        /// </summary>
        private double CalculateTransmissionInterval()
        {
            
            // our mutable "constant" C
            double constantC;

            // our mutable "constant" n
            double constantN;

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
             

            // our mutable "constant" tMin

            // check if the number of senders is less than or
            // equal to 25% of the number of members
            if (this._senders <= 0.25 * this._members && this._rtcp_bw != 0)
            {
                // Checks whether or not we sent the packet
                if (this._we_sent)
                {
                    constantC = this._avg_rtcp_size / (RTCP_SENDER_BW_FRACTION * this._rtcp_bw);
                    constantN = this._senders;
                } 
                else
                {
                    constantC = _avg_rtcp_size / (RTCP_RCVR_BW_FRACTION * this._rtcp_bw);
                    constantN = this._members - this._senders;
                }
            } 
            else if (this._rtcp_bw != 0)
            {
                // cast to double to perform floating point division
                constantC = (double)this._avg_rtcp_size / this._rtcp_bw;
                constantN = this._members;
            }
            else
            {
                throw new ArithmeticException("Cannot divide by zero!");
            }

            // sets the constant Tmin
            var minimumTime = (this._initial) ? 2.5 : 5;

            // calculates our interval Td
            var deterministicCalculatedInterval = Math.Max(minimumTime, constantN*constantC);

            // initializes a new random
            var random = new Random();

            // sets the calculated interval to a number uniformly distributed between 0.5 and 1.5 times
            // the deterministic calculated interval
            this._calculatedInterval = random.NextDouble() * deterministicCalculatedInterval * 1.5 + 0.5 * deterministicCalculatedInterval;

            // The resulting value is divided by a constant to compensate for the fact the timer
            // reconsideration algorithm converges to a value of the RTCP bandwidth below the
            // intended average.
            this._calculatedInterval /= Math.E - ((double) 3 / 2);

            return _calculatedInterval;
        }
    }
}