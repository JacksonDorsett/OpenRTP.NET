using RTP.Net.Data;
using RTP.Net.Utils;
using System.IO;

namespace RTP.Net.RTCP
{
    /// <summary>
    /// source description (SDES)
    /// </summary>
    public class RTCP_SDES : RTCPPacket
    {
        public RTCP_SDES(bool padding, byte count, ushort length, uint sRC, SDESItem[] items) : base(padding, count, length)
        {
            SRC = sRC;
            this.items = items;
        }

        /// <summary>
        /// first SSRC/CSRC
        /// </summary>
        public uint SRC { get; set; }

        /// <summary>
        /// list of SDES items
        /// </summary>
        public SDESItem[] items { get; private set; }

        public override RTCPType Type => RTCPType.SDES;

        public override PacketType PacketType => PacketType.RTCP;

        public override byte[] Serialize()
        {
            base.Serialize();
            byte[] b;
            using (var writer = new MemoryStream())
            {
                writer.Write(NetworkSerializer.Serialize(SRC));
                foreach(var i in items)
                {
                    writer.Write(i.Serialize());
                }
                b = writer.ToArray();
            }
            return b;
        }
    }
}
