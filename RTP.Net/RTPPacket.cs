namespace RTP.Net
{
    class RTPPacket
    {
        public RTPPacket(RTPHeader header, byte[] data)
        {
            this.Header = header;
            this.Data = data;
        }
        public RTPHeader Header { get; private set; }
        public byte[] Data { get; private set; }
    }
}
