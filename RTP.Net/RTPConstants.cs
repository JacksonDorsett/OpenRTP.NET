using RTP.Net.RTCP;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net
{
    class RTPConstants
    {
        public static readonly RTPVersion VERSION = RTPVersion.Two;

        public static readonly uint RTP_SEQ_MOD = (1 << 16);

        public static readonly uint RTP_MAX_SDS = 255;

        public static readonly ushort RTCP_VALID_MASK = (0xc000 | 0x2000 | 0xfe);

        public static readonly uint RTCP_VALID_VALUE = (((uint)VERSION << 14) | (uint)RTCPType.SR);
    }
}
