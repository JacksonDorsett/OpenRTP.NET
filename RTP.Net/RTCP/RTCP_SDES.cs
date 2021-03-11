using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net.RTCP
{
    /// <summary>
    /// source description (SDES)
    /// </summary>
    class RTCP_SDES : RTCP_Body
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
    }
}
