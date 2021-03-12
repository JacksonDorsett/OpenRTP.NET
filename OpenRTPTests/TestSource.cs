using NUnit.Framework;
using RTP.Net.RTCP;
using RTP.Net;

namespace OpenRTPTests
{
    public class TestSource
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestInitialization()
        {
            ushort seq = 10;
            Source s = new Source(seq);
            Assert.AreEqual(seq, s.BaseSequence);
            Assert.AreEqual(seq, s.MaxSequence);
            Assert.AreEqual(RTPConstants.RTP_SEQ_MOD + 1, s.BadSequence);
            // add additional instantiation logic
        }
    }
}