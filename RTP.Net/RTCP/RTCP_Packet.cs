namespace RTP.Net.RTCP
{
    using System.Net.Sockets;
    /// <summary>
    /// One RTCP packet.
    /// </summary>
    class RTCP_Packet
    {
        
        public RTCP_Packet(RTCPHeader header, RTCP_Body body)
        {
            this.Header = header;
            this.Body = body;
        }

        public RTCPHeader Header { get; private set; }

        public RTCP_Body Body { get; private set; }

        // may need later...
        //RTCPType Type { get => Header.Type; }

    }
}
