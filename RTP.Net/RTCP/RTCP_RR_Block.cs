using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net.RTCP
{
    /// <summary>
    /// RTCP Reception report block
    /// </summary>
    class RTCP_RR_Block
    {
        private PacketsLost mLost;
        public RTCP_RR_Block(uint ssrc, byte fraction, uint lost, uint lastSeq, uint jitter, uint lastsrc, uint lastDelay)
        {
            this.SSRC = ssrc;
            this.Fraction = fraction;
            this.mLost = new PacketsLost(lost);
            this.LastSequence = lastSeq;
            this.Jitter = jitter;
            this.LastSource = lastsrc;
            this.DelayLastSource = lastDelay;
        }

        /// <summary>
        /// /* data source being reported */
        /// </summary>
        public uint SSRC { get; private set; }

        /// <summary>
        /// /* fraction lost since last SR/RR */
        /// </summary>
        public byte Fraction { get; private set; }

        //uint lost;  
        /// <summary>
        /// cumulative number of packets lost (signed!) -- 24 bytes
        /// </summary>
        public int Lost
        {
            get
            {
                return mLost.Lost;
            }
        }

        /// <summary>
        /// /* extended last seq. no. received */
        /// </summary>
        public uint LastSequence { get; private set; }

        /// <summary>
        /// /* interarrival jitter */
        /// </summary>
        public uint Jitter { get; private set; }

        /// <summary>
        /// last SR packet from this source */
        /// </summary>
        public uint LastSource { get; private set; }

        /// <summary>
        /// /* delay since last SR packet */
        /// </summary>
        public uint DelayLastSource { get; private set; }             
    }
}
