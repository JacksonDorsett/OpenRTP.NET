namespace RTP.Net.RTCP
{
    class ReceptionReport : RTCP_Body
    {
        public ReceptionReport(uint sSRC, RTCP_RR_Block[] rR_Block_list)
        {
            SSRC = sSRC;
            RR_Block_list = rR_Block_list;
        }

        /// <summary>
        /// receiver generating this report.
        /// </summary>
        public uint SSRC { get; private set; }

        /// <summary>
        /// List of Reception report blocks.
        /// </summary>
        public RTCP_RR_Block[] RR_Block_list { get; private set; }
    }
}
