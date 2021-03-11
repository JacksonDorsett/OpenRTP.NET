using System;

namespace RTP.Net
{
    public class RTPHeader
    {

        public RTPHeader(RTPVersion version, bool padding, bool extension, byte csrcCount, bool marker, byte payloadType, ushort sequenceNum, uint timeStamp, uint ssrc, uint[] csrcList)
        {
            this.Version = version;
            this.Padding = padding;
            this.Extension = extension;
            this.CSRCCount = csrcCount;
            this.Marker = marker;
            this.PayloadType = payloadType;
            this.SequenceNumber = sequenceNum;
            this.Timestamp = timeStamp;
            this.SSRC = ssrc;
            this.CSRCList = csrcList;
        }

        public RTPVersion Version { get; private set; }

        public bool Padding { get; private set; }

        public bool Extension { get; private set; }

        public byte CSRCCount
        {
            get
            {
                return CSRCCount;
            }
            private set
            {
                if (value > 15) throw new ArgumentOutOfRangeException($"CSRC Count must be less than 16 value set was: {value}");
            }
        }

        public bool Marker { get; private set; }


        /// <summary>
        /// A receiver |MUST| ignore packets with payload types that it does not understand.
        /// 
        /// </summary>
        public byte PayloadType
        {
            get
            {
                return PayloadType;
            }
            private set
            {
                if (value > 127) throw new ArgumentOutOfRangeException($"Payload type cannot be greater than 127 but was: {value}");
            }
        }

        /// <summary>
        /// The initial value of the sequence number
        /// SHOULD be random(unpredictable) to make known-plaintext attacks
        /// on encryption more difficult, even if the source itself does not
        /// encrypt according to the method in Section 9.1, because the
        /// packets may flow through a translator that does.
        /// </summary>
        public ushort SequenceNumber { get; private set; }

        /// <summary>
        /// The sampling instant MUST be derived from a
        /// clock that increments monotonically and linearly in time to allow
        /// synchronization and jitter calculations
        /// Theresolution of the clock MUST be sufficient for the desired
        /// synchronization accuracy and for measuring packet arrival jitter
        /// (one tick per video frame is typically not sufficient)
        /// </summary>
        public uint Timestamp { get; private set; }

        /// <summary>
        /// The SSRC field identifies the synchronization source.  This
        /// identifier SHOULD be chosen randomly, with the intent that no two
        /// synchronization sources within the same RTP session will have the
        /// same SSRC identifier.
        /// Although the probability of multiple sources choosing the same identifier is
        /// low, all RTP implementations must be prepared to detect and
        /// resolve collisions.
        /// </summary>
        public uint SSRC { get; private set; }

        public uint[] CSRCList
        {
            get
            {
                return CSRCList;
            }
            set
            {
                if (value.Length != CSRCCount) throw new ArgumentOutOfRangeException("Length of CSRC list does not match given parameter");
                CSRCList = value;
            }
        }





    }
}
