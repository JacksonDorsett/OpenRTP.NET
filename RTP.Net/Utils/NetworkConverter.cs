using System;

namespace RTP.Net.Utils
{
    class NetworkConverter
    {
        public static ushort ToNetworkOrder(ushort data)
        {
            var v = BitConverter.GetBytes(data);
            if (BitConverter.IsLittleEndian) Array.Reverse(v);
            return BitConverter.ToUInt16(v);
        }
    }
}
