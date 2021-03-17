using RTP.Net.Utils;
using System.IO;

namespace RTP.Net.RTCP
{
    public class RTCP_BYE : RTCP_Body
    {
        public RTCP_BYE(uint[] sRC)
        {
            SRC = sRC;
        }

        /// <summary>
        /// List of sources.
        /// </summary>
        public uint[] SRC { get; private set; }

        public override byte[] Serialize()
        {
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
