using System;

namespace RTP.Net.Utils
{
    internal class NetworkSerializer
    {
        public static byte[] Serialize(uint n)
        {
            var b = BitConverter.GetBytes(n);
            if (BitConverter.IsLittleEndian) Array.Reverse(b);
            return b;
        }
    }
}
