namespace RTP.Net.RTCP
{
    public abstract class RTCP_Body : ISerialize
    {
        public abstract byte[] Serialize();
    }
}