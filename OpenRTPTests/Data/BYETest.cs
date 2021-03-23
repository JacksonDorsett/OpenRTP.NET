using NUnit.Framework;
using RTP.Net.RTCP;
namespace OpenRTPTests.Data
{
    class BYETest
    {
        [Test]
        public void TestByeSerialization()
        {
            uint[] a = { 1211515, 512534 };
            var b = new RTCP_BYE(false, 0,0, a);
            var t = b.Serialize();
            Assert.AreEqual(12, t.Length);
            Assert.AreEqual(18, t[1]);
            Assert.AreEqual(210, t[6]);
        }
    }
}
