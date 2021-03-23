using RTP.Net.Data;
using RTP.Net.Utils;
using System.IO;

namespace RTP.Net.RTCP
{
    public class RTCP_BYE : RTCPPacket
    {
        public RTCP_BYE(bool padding, byte count, ushort length, uint[] ssrc) : base(padding, count, length)
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
            
            byte[] b = new byte[4 * SRC.Length];
            using (BinaryWriter bw = new BinaryWriter(new MemoryStream()))
            {
                bw.Write(base.Serialize());
                foreach (uint i in SRC)
                {
                    
                    bw.Write(NetworkSerializer.Serialize(i));
                }
            }


            return b;
        }

    }
}
