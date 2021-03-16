using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RTP.Net.RTCP;
namespace OpenRTPTests.Data
{
    class ReceptionBlockTest
    {
        [Test]
        public void TestSerialize()
        {
            RTCP_RR_Block rr = new RTCP_RR_Block(100, 42, 6235, 1000, 192, 4, 7);
            var b = rr.Serialize();
            //SSRC test
            Assert.AreEqual(100, b[3]);
        }
    }
}
