using RTP.Net.RTCP;
using RTP.Net.Utils;
using System;

namespace RTP.Net
{
    public class RTPHeader
    {
        byte mCSRCCount;
        uint[] mCSRCList;
        byte mPayloadType;
        public RTPHeader(byte version, bool padding, bool extension, byte csrcCount, bool marker, byte payloadType, ushort sequenceNum, uint timeStamp, uint ssrc, uint[] csrcList)
        {
            //RTP header validity checks
            if (version != 2) throw new FormatException("Header version must be 2");
            if (payloadType == (byte)RTCPType.SR || payloadType == (byte)RTCPType.RR) throw new FormatException("payload type must not be RR or SR");

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

        public byte Version { get; private set; }

        public bool Padding { get; private set; }

        public bool Extension { get; private set; }

        public byte CSRCCount
        {
            get
            {
                return mCSRCCount;
            }
            private set
            {
                if (value > 15) throw new ArgumentOutOfRangeException("CSRC Count must be less than 16");
                this.mCSRCCount = value;
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
                return mPayloadType;
            }
            private set
            {
                if (value > 127) throw new ArgumentOutOfRangeException($"Payload type cannot be greater than 127 but was: {value}");
                mPayloadType = value;
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
                return mCSRCList;
            }
            set
            {
                if (value.Length != CSRCCount) throw new ArgumentOutOfRangeException("Length of CSRC list does not match given parameter");
                mCSRCList = value;
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
            return ret;
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
