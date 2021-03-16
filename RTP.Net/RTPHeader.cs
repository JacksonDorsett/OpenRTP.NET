using RTP.Net.RTCP;
using RTP.Net.Utils;
using System;

namespace RTP.Net
{
    public class RTPHeader
    {

        /// <summary>
        ///     The CSRC count contains the number of CSRC identifiers that follow
        ///     the fixed header.
        ///     (https://tools.ietf.org/html/rfc3550)
        /// </summary>
        private byte _csrcCount;

        /// <summary>
        ///     If the padding bit is set, the packet contains one or more
        ///     additional padding octets at the end which are not part of the
        ///     payload.  The last octet of the padding contains a count of how
        ///     many padding octets should be ignored, including itself.  Padding
        ///     may be needed by some encryption algorithms with fixed block sizes
        ///     or for carrying several RTP packets in a lower-layer protocol data
        ///     unit. (https://tools.ietf.org/html/rfc3550)
        /// </summary>
        private uint[] _csrcList;

        /// <summary>
        ///     This field identifies the format of the RTP payload and determines
        ///     its interpretation by the application.  A profile MAY specify a
        ///     default static mapping of payload type codes to payload formats.
        ///     Additional payload type codes MAY be defined dynamically through
        ///     non-RTP means (see Section 3).  A set of default mappings for
        ///     audio and video is specified in the companion RFC 3551 [1].  An
        ///     RTP source MAY change the payload type during a session, but this
        ///     field SHOULD NOT be used for multiplexing separate media streams
        ///     (see Section 5.2).
        ///     A receiver MUST ignore packets with payload types that it does not
        ///     understand.  (https://tools.ietf.org/html/rfc3550)
        /// </summary>
        private byte _PayloadType;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RTPHeader" /> class.
        /// </summary>
        /// <param name="version">The version of RTP that is being used.</param>
        /// <param name="padding">The padding bit.</param>
        /// <param name="extension">An extension bit.</param>
        /// <param name="csrcCount">The CSRC count that contains the number of CSRC identifiers that follow the fixed header.</param>
        /// <param name="marker">Marker bits.</param>
        /// <param name="payloadType">Specifies the type of the payload</param>
        /// <param name="sequenceNum">A sequence number.</param>
        /// <param name="timeStamp">A timestamp that reflects the sampling instant of the first octet in the RTP data packet.</param>
        /// <param name="ssrc">The SSRC field identifies a synchronization source.</param>
        /// <param name="csrcList"> The CSRC list identifies the contributing sources for the payload contained in this packet.</param>
        public RTPHeader(RTPVersion version, bool padding, bool extension, byte csrcCount, bool marker,
            byte payloadType, ushort sequenceNum, uint timeStamp, uint ssrc, uint[] csrcList)
        {
            //This field identifies the version of RTP.  The version defined by
            // this specification is two (2).  (The value 1 is used by the first
            // draft version of RTP and the value 0 is used by the protocol
            // initially implemented in the "vat" audio tool.)
            // (https://tools.ietf.org/html/rfc3550)
            Version = version;

            // If the padding bit is set, the packet contains one or more
            // additional padding octets at the end which are not part of the
            // payload.  The last octet of the padding contains a count of how
            // many padding octets should be ignored, including itself.  Padding
            // may be needed by some encryption algorithms with fixed block sizes
            // or for carrying several RTP packets in a lower-layer protocol data
            // unit.
            // (https://tools.ietf.org/html/rfc3550)
            Padding = padding;

            // If the extension bit is set, the fixed header MUST be followed by
            // exactly one header extension, with a format defined in Section
            // 5.3.1. (https://tools.ietf.org/html/rfc3550)
            Extension = extension;

            // The CSRC count contains the number of CSRC identifiers that follow
            // the fixed header
            // (https://tools.ietf.org/html/rfc3550)
            CSRCCount = csrcCount;

            // The interpretation of the marker is defined by a profile.  It is
            // intended to allow significant events such as frame boundaries to
            // be marked in the packet stream.  A profile MAY define additional
            // marker bits or specify that there is no marker bit by changing the
            // number of bits in the payload type field (see Section 5.3).
            // (https://tools.ietf.org/html/rfc3550)
            Marker = marker;

            // This field identifies the format of the RTP payload and determines
            // its interpretation by the application.  A profile MAY specify a
            // default static mapping of payload type codes to payload formats.
            // Additional payload type codes MAY be defined dynamically through
            // non-RTP means (see Section 3).  A set of default mappings for
            // audio and video is specified in the companion RFC 3551 [1].  An
            // RTP source MAY change the payload type during a session, but this
            // field SHOULD NOT be used for multiplexing separate media streams
            // (see Section 5.2).
            // 
            // A receiver MUST ignore packets with payload types that it does not
            // understand.
            // (https://tools.ietf.org/html/rfc3550)
            PayloadType = payloadType;

            // The sequence number increments by one for each RTP data packet
            // sent, and may be used by the receiver to detect packet loss and to
            // restore packet sequence.  The initial value of the sequence number
            // SHOULD be random (unpredictable) to make known-plaintext attacks
            // on encryption more difficult, even if the source itself does not
            // encrypt according to the method in Section 9.1, because the
            // packets may flow through a translator that does.  Techniques for
            // choosing unpredictable numbers are discussed in [17].
            // (https://tools.ietf.org/html/rfc3550)
            SequenceNumber = sequenceNum;

            // he timestamp reflects the sampling instant of the first octet in
            // the RTP data packet.  The sampling instant MUST be derived from a
            // clock that increments monotonically and linearly in time to allow
            // synchronization and jitter calculations (see Section 6.4.1).  The
            // resolution of the clock MUST be sufficient for the desired
            // synchronization accuracy and for measuring packet arrival jitter
            // (one tick per video frame is typically not sufficient).  The clock
            // frequency is dependent on the format of data carried as payload
            // and is specified statically in the profile or payload format
            // specification that defines the format, or MAY be specified
            // dynamically for payload formats defined through non-RTP means.
            // (https://tools.ietf.org/html/rfc3550)
            Timestamp = timeStamp;

            // The SSRC field identifies the synchronization source.  This
            // identifier SHOULD be chosen randomly, with the intent that no two
            // synchronization sources within the same RTP session will have the
            // same SSRC identifier.  An example algorithm for generating a
            // random identifier is presented in Appendix A.6.  Although the
            // probability of multiple sources choosing the same identifier is
            // low, all RTP implementations must be prepared to detect and
            // resolve collisions.  Section 8 describes the probability of
            // collision along with a mechanism for resolving collisions and
            // detecting RTP-level forwarding loops based on the uniqueness of
            // the SSRC identifier.  If a source changes its source transport
            // address, it must also choose a new SSRC identifier to avoid being
            // interpreted as a looped source (see Section 8.2).
            // (https://tools.ietf.org/html/rfc3550)
            SSRC = ssrc;

            // The CSRC list identifies the contributing sources for the payload
            // contained in this packet.  The number of identifiers is given by
            // the CC field.  If there are more than 15 contributing sources,
            // only 15 can be identified.  CSRC identifiers are inserted by
            // mixers (see Section 7.1), using the SSRC identifiers of
            // contributing sources.  For example, for audio packets the SSRC
            // identifiers of all sources that were mixed together to create a
            // packet are listed, allowing correct talker indication at the
            // receiver.
            // (https://tools.ietf.org/html/rfc3550)
            CSRCList = csrcList;
        }

        /// <summary>
        ///     Gets the payload version.
        ///     This field identifies the version of RTP.  The version defined by
        ///     this specification is two (2).  (The value 1 is used by the first
        ///     draft version of RTP and the value 0 is used by the protocol
        ///     initially implemented in the "vat" audio tool.)
        ///     (https://tools.ietf.org/html/rfc3550)
        /// </summary>
        public RTPVersion Version { get; }


        /// <summary>
        ///     If the padding bit is set, the packet contains one or more
        ///     additional padding octets at the end which are not part of the
        ///     payload.  The last octet of the padding contains a count of how
        ///     many padding octets should be ignored, including itself.  Padding
        ///     may be needed by some encryption algorithms with fixed block sizes
        ///     or for carrying several RTP packets in a lower-layer protocol data
        ///     unit. (https://tools.ietf.org/html/rfc3550)
        /// </summary>
        public bool Padding { get; }

        /// <summary>
        ///     If the extension bit is set, the fixed header MUST be followed by
        ///     exactly one header extension, with a format defined in Section
        ///     5.3.1. (https://tools.ietf.org/html/rfc3550)
        /// </summary>
        public bool Extension { get; }

        /// <summary>
        ///     The CSRC count contains the number of CSRC identifiers that follow
        ///     the fixed header. (https://tools.ietf.org/html/rfc3550)
        /// </summary>
        public byte CSRCCount
        {
            get => _csrcCount;
            private set
            {
                if (value > 15)
                    throw new ArgumentOutOfRangeException($"CSRC Count must be less than 16 value set was: {value}");

                _csrcCount = value;
            }
        }

        /// <summary>
        ///     The interpretation of the marker is defined by a profile.  It is
        ///     intended to allow significant events such as frame boundaries to
        ///     be marked in the packet stream.  A profile MAY define additional
        ///     marker bits or specify that there is no marker bit by changing the
        ///     number of bits in the payload type field (see Section 5.3).
        ///     (https://tools.ietf.org/html/rfc3550)
        /// </summary>
        public bool Marker { get; private set; }

        /// <summary>
        ///     A receiver |MUST| ignore packets with payload types that it does not understand.
        ///     This field identifies the format of the RTP payload and determines
        ///     its interpretation by the application.  A profile MAY specify a
        ///     default static mapping of payload type codes to payload formats.
        ///     Additional payload type codes MAY be defined dynamically through
        ///     non-RTP means (see Section 3).  A set of default mappings for
        ///     audio and video is specified in the companion RFC 3551 [1].  An
        ///     RTP source MAY change the payload type during a session, but this
        ///     field SHOULD NOT be used for multiplexing separate media streams
        ///     (see Section 5.2). (https://tools.ietf.org/html/rfc3550)
        /// </summary>
        public byte PayloadType
        {
            get => _PayloadType;
            private set
            {
                if (value > 127)
                    throw new ArgumentOutOfRangeException($"Payload type cannot be greater than 127 but was: {value}");
                _PayloadType = value;
            }
        }

        /// <summary>
        ///     The initial value of the sequence number
        ///     SHOULD be random(unpredictable) to make known-plaintext attacks
        ///     on encryption more difficult, even if the source itself does not
        ///     encrypt according to the method in Section 9.1, because the
        ///     packets may flow through a translator that does.
        /// </summary>
        public ushort SequenceNumber { get; private set; }

        /// <summary>
        ///     The sampling instant MUST be derived from a
        ///     clock that increments monotonically and linearly in time to allow
        ///     synchronization and jitter calculations
        ///     The resolution of the clock MUST be sufficient for the desired
        ///     synchronization accuracy and for measuring packet arrival jitter
        ///     (one tick per video frame is typically not sufficient)
        /// </summary>
        public uint Timestamp { get; private set; }

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
        ///     The CSRC list identifies the contributing sources for the payload
        ///     contained in this packet.  The number of identifiers is given by
        ///     the CC field.  If there are more than 15 contributing sources,
        ///     only 15 can be identified.  CSRC identifiers are inserted by
        ///     mixers (see Section 7.1), using the SSRC identifiers of
        ///     contributing sources.  For example, for audio packets the SSRC
        ///     identifiers of all sources that were mixed together to create a
        ///     packet are listed, allowing correct talker indication at the
        ///     receiver. (https://tools.ietf.org/html/rfc3550)
        /// </summary>
        public uint[] CSRCList
        {
            get => _csrcList;
            set
            {
                if (value.Length != CSRCCount)
                    throw new ArgumentOutOfRangeException("Length of CSRC list does not match given parameter");
                _csrcList = value;
            }

        }


        public uint Length
        {
            get
            {
                return 96 + 32 * (uint)this.CSRCList.Length;
            }
        }

        public byte[] Serialize()
        {
            byte[] ret = new byte[this.Length];
            ret[0] |= 2 << 6;
            ret[0] |= (byte)(Convert2Byte(this.Padding) << 5);
            ret[0] |= (byte)(Convert2Byte(this.Extension) << 4);
            ret[0] |= this.CSRCCount;

            ret[1] |= (byte)(Convert2Byte(this.Extension) << 7);
            ret[1] |= PayloadType;
            // Serialize sequence order
            SerializeSequenceNum(ret);
            SerializeTimestamp(ret);
            SerializeSSRC(ret);
            SerializeCSRC(ret);
            return ret;
        }

        private void SerializeCSRC(byte[] ret)
        {
            int offset = 12;
            foreach (var c in this.CSRCList)
            {
                SerializeNetworkInt(ret, c, offset);
                offset += 4;
            }
        }

        private void SerializeNetworkInt(byte[] a, uint n, int offset)
        {
            var ts = BitConverter.GetBytes(n);
            if (BitConverter.IsLittleEndian) Array.Reverse(ts);
            for (int i = 0; i < ts.Length; i++)
            {
                a[offset + i] = ts[i];
            }
        }

        private void SerializeTimestamp(byte[] ret)
        {
            byte TIMESTAMP_OFFSET = 4;
            var ts = BitConverter.GetBytes(this.Timestamp);
            if (BitConverter.IsLittleEndian) Array.Reverse(ts);
            for(int i = 0; i < ts.Length; i++)
            {
                ret[TIMESTAMP_OFFSET + i] = ts[i];
            }
        }

        private void SerializeSSRC(byte[] ret)
        {
            byte SSRC_OFFSET = 8;
            var ts = BitConverter.GetBytes(this.SSRC);
            if (BitConverter.IsLittleEndian) Array.Reverse(ts);
            for (int i = 0; i < ts.Length; i++)
            {
                ret[SSRC_OFFSET + i] = ts[i];
            }
        }
        private void SerializeSequenceNum(byte[] ret)
        {
            var sn = NetworkConverter.ToNetworkOrder(this.SequenceNumber);
            ret[2] = BitConverter.GetBytes(sn)[0];
            ret[3] = BitConverter.GetBytes(sn)[1];
        }

        private byte Convert2Byte(bool b)
        {
            return b == true ? (byte)1 : (byte)0;
        }
    }
}