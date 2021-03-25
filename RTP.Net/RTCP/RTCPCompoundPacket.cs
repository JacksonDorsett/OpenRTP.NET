using System;
using System.IO;
using RTP.Net.Utils;

namespace RTP.Net.RTCP
{
    using System.Collections.Generic;
    
    /// <summary>
    ///     Class representing the compound rtcp packet.
    /// </summary>
    public class RTCPCompoundPacket : RTCPPacket
    {
        private readonly List<RTCPPacket> _packetList;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RTCPCompoundPacket" /> class.
        /// </summary>
        /// <param name="padding">The padding bit.</param>
        /// <param name="count">The byte representing the count.</param>
        /// <param name="length">The length of the RTCP packet.</param>
        /// <param name="packets">An RTCP_SDES packet.</param>
        public RTCPCompoundPacket(List<RTCPPacket> packets, bool padding, byte count, ushort length) : base(padding, count, length)
        {
            if (packets[0].Type == RTCPType.SR || packets[0].Type == RTCPType.RR)
            {
                this._packetList = packets;
            }

            throw new ArgumentOutOfRangeException($"You cannot pass a packet of this type: {packets[0].Type}");
        }
        
        /// <summary>
        ///     Serialization function.
        /// </summary>
        /// <returns>The serialization of the aforementioned.</returns>
        public override byte[] Serialize()
        {
            base.Serialize();
            using var writer = new MemoryStream();
            foreach (var packet in this._packetList)
            {
                writer.Write(packet.Serialize());
            }
            return writer.ToArray();
        }
        
        /// <summary>
        ///     Gets the RTCPType that is associated with the RTCPCompoundPacket
        /// </summary>
        public override RTCPType Type { get; }
    }
}