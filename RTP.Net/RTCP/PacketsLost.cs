using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net.RTCP
{  
    /// <summary>
    /// Represents the 24 bit packet lost field
    /// </summary>
    class PacketsLost
    {
        bool positive;
        private uint mLost;
        public PacketsLost(uint n)
        {
            if (n >= (1 << 24)) throw new ArgumentOutOfRangeException("Packet Loss Exception: input must be less than 25 bytes");
            uint mask = 0x7FFFF;
            if ((1 << 23 & n) == 1)
            {
                positive = true;
                mLost = (n | mask);
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
    }
}
