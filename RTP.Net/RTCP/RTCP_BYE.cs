using RTP.Net.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RTP.Net.RTCP
{
    public class RTCP_BYE : RTCP_Body, ISerialize
    {
        public RTCP_BYE(uint[] sRC)
        {
            SRC = sRC;
        }

        /// <summary>
        /// List of sources.
        /// </summary>
        public uint[] SRC { get; private set; }

        public byte[] Serialize()
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
