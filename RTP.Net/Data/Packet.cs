using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net.Data
{
    public abstract class Packet : ISerialize
    {
        public abstract byte[] Serialize();

        public abstract PacketType PacketType { get; }
    }
}
