namespace RTP.Net.RTCP
{
    using System.Collections.Generic;
    
    /// <summary>
    ///     Class representing the compound rtcp packet.
    /// </summary>
    public class RTCPCompoundPacket : RTCPPacket
    {
        private List<RTCPPacket> _packetList;
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="RTCPCompoundPacket" /> class.
        /// </summary>
        /// <param name="padding">The padding bit.</param>
        /// <param name="count">The byte representing the count.</param>
        /// <param name="length">The length of the RTCP packet.</param>
        public RTCPCompoundPacket(bool padding, byte count, ushort length) : base(padding, count, length)
        {
        }
        
        /// <summary>
        ///     Gets the RTCPType that is associated with the RTCPCompoundPacket
        /// </summary>
        public override RTCPType Type { get; }
    }
}