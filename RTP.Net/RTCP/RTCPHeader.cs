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
        public RTCPHeader(bool padding, byte count, RTCPType type, uint length, uint ssrc, uint timeStamp,
            uint packetCount, uint octetCount)
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
            this.ReceptionReportCount = count;
            this.Type = type;
            this.Length = length;
            this.SSRC = ssrc;
            this.TimeStamp = timeStamp;
            this.PacketCount = packetCount;
            this.OctetCount = octetCount;
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
        ///     The number of reception report blocks contained in this packet.  A
        ///     value of zero is valid.
        /// </summary>
        protected int ReceptionReportCount
        {
            get => this._count;

            private set
            {
                if (value >= 1 << 5)
                    throw new ArgumentOutOfRangeException("Count", value, "input data must be 5 bytes");
                this._count = value;
            }
        }

        /// <summary>
        ///     The SSRC field identifies the synchronization source.  This
        ///     identifier SHOULD be chosen randomly, with the intent that no two
        ///     synchronization sources within the same RTP session will have the
        ///     same SSRC identifier.
        ///     Although the probability of multiple sources choosing the same identifier is
        ///     low, all RTP implementations must be prepared to detect and
        ///     resolve collisions.
        /// </summary>
        public uint SSRC { get; private set; }

        /// <summary>
        ///     Gets the type of the RTCP packet.
        /// </summary>
        public RTCPType Type { get; private set; }

        /// <summary>
        ///     Gets the length of the RTCP packet.
        /// </summary>
        public uint Length { get; private set; }

        /// <summary>
        ///     Corresponds to the same time as the NTP timestamp (above), but in
        ///     the same units and with the same random offset as the RTP
        ///     timestamps in data packets.  This correspondence may be used for
        ///     intra- and inter-media synchronization for sources whose NTP
        ///     timestamps are synchronized, and may be used by media-independent
        ///     receivers to estimate the nominal RTP clock frequency.  Note that
        ///     in most cases this timestamp will not be equal to the RTP
        ///     timestamp in any adjacent data packet.  Rather, it MUST be
        ///     calculated from the corresponding NTP timestamp using the
        ///     relationship between the RTP timestamp counter and real time as
        ///     maintained by periodically checking the wallclock time at a
        ///     sampling instant.
        /// </summary>
        protected uint TimeStamp { get; private set; }

        /// <summary>
        ///     The total number of RTP data packets transmitted by the sender
        ///     since starting transmission up until the time this SR packet was
        ///     generated.  The count SHOULD be reset if the sender changes its
        ///     SSRC identifier
        /// </summary>
        protected uint PacketCount { get; private set; }

        /// <summary>
        ///     The total number of payload octets (i.e., not including header or
        /// padding) transmitted in RTP data packets by the sender since
        /// starting transmission up until the time this SR packet was
        /// generated.  The count SHOULD be reset if the sender changes its
        /// SSRC identifier.  This field can be used to estimate the average
        /// payload data rate.
        /// </summary>
        protected uint OctetCount { get; private set; }
    }
}