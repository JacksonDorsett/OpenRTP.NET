using RTP.Net.Data;
using RTP.Net.Utils;
using System.IO;

namespace RTP.Net.RTCP
{
    internal class RTCP_BYE : RTCPPacket
    {
        public RTCP_BYE(bool padding, byte count, uint length, uint[] ssrc) : base(padding, count, length)
        {
            this.SRC = ssrc;
        }

        /// <summary>
        /// List of sources.
        /// </summary>
        public uint[] SRC { get; private set; }

        public override RTCPType Type => RTCPType.BYE;

        public override PacketType PacketType => throw new System.NotImplementedException();

        public override byte[] Serialize()
        {
            base.Serialize();
            byte[] b = new byte[4 * SRC.Length];
            using (BinaryWriter bw = new BinaryWriter(new MemoryStream(b)))
            {
                foreach (uint i in SRC)
                {
                    bw.Write(NetworkSerializer.Serialize(i));
                }
            }


            return b;
        }

    }
}
