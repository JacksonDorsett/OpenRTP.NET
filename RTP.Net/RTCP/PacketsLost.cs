using RTP.Net.Utils;
using System;
using System.Collections.Generic;

namespace RTP.Net.RTCP
{
    /// <summary>
    /// Represents the 24 bit packet lost field
    /// </summary>
    class PacketsLost : ISerialize
    {
        bool positive;
        private uint mLost;
        public PacketsLost(uint n)
        {
            if (n >= (1 << 24)) throw new ArgumentOutOfRangeException("Packet Loss Exception: input must be less than 25 bytes");
            uint mask = 0x7FFFF;
            if ((1 << 23 & n) == 1)
            {
                positive = false;
                mLost = (n | mask);
            }
            else
            {
                positive = true;
                mLost = n;
            }
        }

        public int Lost
        {
            get
            {
                if (positive) return (int)mLost;
                return -(int)mLost;
            }
        }

        public byte[] Serialize()
        {
            List<byte> l = new List<byte>(NetworkSerializer.Serialize((uint)Lost));
            l.RemoveAt(0);
            //l.RemoveAt(0);
            return l.ToArray();

        }
    }
}
