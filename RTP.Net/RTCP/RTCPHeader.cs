using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net.RTCP
{
    /// <summary>
    /// RTCP common header word.
    /// </summary>
    public class RTCPHeader
    {
        public RTCPHeader(bool padding, byte count, RTCPType type, uint length)
        {
            Padding = padding;
            Count = count;
            Type = type;
            Length = length;
        }

        RTPVersion Version { get => RTPVersion.Two; } //protocol version

        public bool Padding { get; private set; } //padding flag

        public byte Count //varies by packet type
        {
            get
            {
                return Count;
            }
            private set
            {
                if (value >= 1 << 5) throw new ArgumentOutOfRangeException("input data must be 5 bytes");
                Count = value;
            }
        }

        public RTCPType Type { get; private set; } //RTCP packet type

        public uint Length { get; private set; } //pkt len in words, w/o this word
    }
}
