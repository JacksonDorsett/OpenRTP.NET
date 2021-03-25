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
        /// <param name="receptionReport"></param>
        /// <param name="padding">The padding bit.</param>
        /// <param name="count">The byte representing the count.</param>
        /// <param name="length">The length of the RTCP packet.</param>
        /// <param name="packets">An RTCP_SDES packet.</param>
        /// <param name="sdesPacket">A sdes packet.</param>
        /// <param name="byePacket">A bye packet.</param>
        /// <param name="senderReport">A sender report.</param>
        public RTCPCompoundPacket(List<RTCPPacket> packets, RTCP_SDES sdesPacket, RTCP_BYE byePacket, SenderReport senderReport, 
            ReceptionReport receptionReport, bool padding, byte count, ushort length) : base(padding, count, length)
        {
            _packetList = packets;
            SdesPacket = sdesPacket;
            ByePacket = byePacket;
            SenderReport = senderReport;
            ReceptionReport = receptionReport;
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
        protected override RTCPType Type { get; }
        
        /// <summary>
        ///     A SDES packet.
        /// </summary>
        public RTCP_SDES SdesPacket { get; }
        
        /// <summary>
        ///     A Bye packet.
        /// </summary>
        public RTCP_BYE ByePacket { get; }
        
        /// <summary>
        ///     A sender report.
        /// </summary>
        public SenderReport SenderReport { get; }
        
        /// <summary>
        ///     A reception report.
        /// </summary>
        public ReceptionReport ReceptionReport { get; }
    }
}