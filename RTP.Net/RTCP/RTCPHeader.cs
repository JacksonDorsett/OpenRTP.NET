using System;

namespace RTP.Net.RTCP
{
    /// <summary>
    ///     RTCP common header word.
    /// </summary>
    public class RTCPHeader
    {
        /// <summary>
        ///     A field that varies by packet type.
        /// </summary>
        private int _count;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RTCPHeader" /> class.
        /// </summary>
        /// <param name="padding">The padding bit.</param>
        /// <param name="count">The byte representing the count.</param>
        /// <param name="type">The type enum representing the type of the RTCPType.</param>
        /// <param name="length">The length of the RTCP packet.</param>
        public RTCPHeader(bool padding, RTCPType type, uint length)
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
            this.Type = type;
            this.Length = length;
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
        protected bool Padding { get; private set; }

        /// <summary>
        ///     The SSRC field identifies the synchronization source.  This
        ///     identifier SHOULD be chosen randomly, with the intent that no two
        ///     synchronization sources within the same RTP session will have the
        ///     same SSRC identifier.
        ///     Although the probability of multiple sources choosing the same identifier is
        ///     low, all RTP implementations must be prepared to detect and
        ///     resolve collisions.
        /// </summary

        /// <summary>
        ///     Gets the type of the RTCP packet.
        /// </summary>
        public RTCPType Type { get; private set; }

        /// <summary>
        ///     Gets the length of the RTCP packet.
        /// </summary>
        public uint Length { get; private set; }

        
    }
}