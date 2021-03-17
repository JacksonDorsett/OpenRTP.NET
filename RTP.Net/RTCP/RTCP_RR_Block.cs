using RTP.Net.Utils;
using System.IO;

namespace RTP.Net.RTCP
{
    /// <summary>
    /// RTCP Reception report block
    /// </summary>
    public class RTCP_RR_Block : ISerialize
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

        public byte[] Serialize()
        {
            var b = new byte[24];
            using (var writer = new BinaryWriter(new MemoryStream(b)))
            {
                writer.Write(NetworkSerializer.Serialize(SSRC));
                writer.Write(this.Fraction);
                writer.Write(this.mLost.Serialize());
                writer.Write(NetworkSerializer.Serialize(LastSequence));
                writer.Write(NetworkSerializer.Serialize(Jitter));
                writer.Write(NetworkSerializer.Serialize(LastSource));
                writer.Write(NetworkSerializer.Serialize(DelayLastSource));

            }
            return b;
        }
    }
}
