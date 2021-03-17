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
            //test fraction.
            Assert.AreEqual(42, b[4]);
            //loss test
            Assert.AreEqual(24, b[6]);
            Assert.AreEqual(91, b[7]);
            //last seq test
            Assert.AreEqual(3, b[10]);
            Assert.AreEqual(232, b[11]);
            //jitter test
            Assert.AreEqual(192, b[15]);
            // lastsrc test
            Assert.AreEqual(4, b[19]);
            // last delay test
            Assert.AreEqual(7, b[23]);
        }
    }
}
