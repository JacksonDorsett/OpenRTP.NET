using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RTP.Net.RTCP
{
    class SDESItem : ISerialize
    {
        public SDESItem(byte type, byte length, byte[] data)
        {
            //guard clauses
            if (type > 8) throw new NotSupportedException("unsupported SDES type, must be number: 0-8");
            if (length != data.Length) throw new ArgumentException("length must be equal to the length of the data");

            this.Type = (SDESType)type;
            this.Length = length;
            this.data = Encoding.UTF8.GetString(data);
        }
        /// <summary>
        ///  type of item (see SDESType)
        /// </summary>
        public SDESType Type { get; private set; }

        /// <summary>
        /// length of item (in octets)
        /// </summary>
        public byte Length { get; private set; }

        /// <summary>
        /// text, not null-terminated
        /// </summary>
        public string data { get; private set; }

        public byte[] Serialize()
        {
            byte[] b = new byte[2 + Length];
            using (BinaryWriter writer = new BinaryWriter(new MemoryStream(b)))
            {
                writer.Write((byte)this.Type);
                writer.Write(Length);
                writer.Write(Encoding.UTF8.GetBytes(data));
            }
            return b;
        }
    }
}
