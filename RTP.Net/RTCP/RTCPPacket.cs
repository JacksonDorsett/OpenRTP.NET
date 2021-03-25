using RTP.Net.Data;
using RTP.Net.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RTP.Net.RTCP
{
    public abstract class RTCPPacket : Packet
    {
        protected RTCPPacket(bool padding, byte count, ushort length)
        {
            // If the padding bit is set, this individual RTCP packet contains
            // some additional padding octets at the end which are not part of
            // the control information but are included in the length field.  The
            // last octet of the padding is a count of how many padding octets
            // should be ignored, including itself (it will be a multiple of
            // four).  Padding may be needed by some encryption algorithms with
            // fixed block sizes.  In a compound RTCP packet, padding is only
            // required on one individual packet because the compound packet is
            // encrypted as a whole for the method in Section 9.1.  Thus, padding
            // MUST only be added to the last individual packet, and if padding
            // is added to that packet, the padding bit MUST be set only on that
            // packet.  This convention aids the header validity checks described
            // in Appendix A.2 and allows detection of packets from some early
            // implementations that incorrectly set the padding bit on the first
            // individual packet and add padding to the last individual packet.
            // (https://tools.ietf.org/html/rfc3550)
            this.Padding = padding;

            // A field that varies by packet type
            this.Count = count;
            this.Length = length;
            //this.SSRC = ssrc;
        }

        /// <summary>
        ///     The RTP version that we are currently using.
        /// </summary>
        public byte Version { get => 2; }

        /// <summary>
        ///     If the padding bit is set, this individual RTCP packet contains
        ///     some additional padding octets at the end which are not part of
        ///     the control information but are included in the length field.  The
        ///     last octet of the padding is a count of how many padding octets
        ///     should be ignored, including itself (it will be a multiple of
        ///     four).  Padding may be needed by some encryption algorithms with
        ///     fixed block sizes.  In a compound RTCP packet, padding is only
        ///     required on one individual packet because the compound packet is
        ///     encrypted as a whole for the method in Section 9.1.  Thus, padding
        ///     MUST only be added to the last individual packet, and if padding
        ///     is added to that packet, the padding bit MUST be set only on that
        ///     packet.  This convention aids the header validity checks described
        ///     in Appendix A.2 and allows detection of packets from some early
        ///     implementations that incorrectly set the padding bit on the first
        ///     individual packet and add padding to the last individual packet.
        ///     (https://tools.ietf.org/html/rfc3550#section-6.1)
        /// </summary>
        private bool Padding { get; set; }

        private byte Count { get; set; }

        /// <summary>
        ///     Gets the type of the RTCP packet.
        /// </summary>
        protected abstract RTCPType Type { get; }


        /// <summary>
        ///     Gets the length of the RTCP packet.
        /// </summary>
        private ushort Length { get; set; }

        public override PacketType PacketType => PacketType.RTCP;

        public override byte[] Serialize()
        {
            using var ms = new MemoryStream();
            byte[] b = new byte[2];
            b[0] |= 2 << 6;
            b[0] |= (byte)(this.Padding ? 1 : 0);
            b[0] |= Count;
            b[1] = (byte)Type;
            ms.Write(b);
            ms.Write(NetworkSerializer.Serialize(Length));
            //ms.Write(NetworkSerializer.Serialize(SSRC));
            return ms.ToArray();
        }
    }
}
