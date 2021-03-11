using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net.RTCP
{
    class RTCP_BYE : RTCP_Body
    {
        public RTCP_BYE(uint[] sRC)
        {
            SRC = sRC;
        }

        /// <summary>
        /// List of sources.
        /// </summary>
        public uint[] SRC { get; private set; }
    }
}
