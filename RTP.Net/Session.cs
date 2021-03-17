using System;

namespace RTP.Net
{
    internal class Session
    {
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
        private uint _rtcp_bw;

        /// <summary>
        ///     The most current estimate for the number of senders in the session.
        /// </summary>
        private uint _senders;

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
        private bool
            _we_sent;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Session" /> class.
        /// </summary>
        /// <param name="avgRtcpSize">The average RTCP size.</param>
        public Session(uint avgRtcpSize)
        {
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

        /// <summary>
        ///     Calculates the transmission interval of our packets.
        ///     Source: (https://tools.ietf.org/html/rfc3550#section-6.3)
        /// </summary>
        private void CalculateTransmissionInterval()
        {
            // our mutable "constant" C
            double constantC;

            // our mutable "constant" n
            double constantN;

            // check if the number of senders is less than or
            // equal to 25% of the number of members
            if (this._senders <= 0.25 * this._members && this._rtcp_bw != 0)
            {
                // Checks whether or not we sent the packet
                if (this._we_sent)
                {
                    constantC = this._avg_rtcp_size / (0.25 * this._rtcp_bw);
                    constantN = this._senders;
                } 
                else
                {
                    constantC = _avg_rtcp_size / (0.75 * this._rtcp_bw);
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
        }
    }
}