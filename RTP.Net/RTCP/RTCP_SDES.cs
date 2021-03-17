using RTP.Net.Utils;
using System.IO;

namespace RTP.Net.RTCP
{
    /// <summary>
    /// source description (SDES)
    /// </summary>
    public class RTCP_SDES : RTCP_Body, ISerialize
    {
        public RTCP_SDES(uint sRC, SDESItem[] items)
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

        public byte[] Serialize()
        {
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
