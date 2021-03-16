using NUnit.Framework;
using RTP.Net.RTCP;
using RTP.Net;

namespace OpenRTPTests
{
    public class TestSource
    {
        private static uint MIN_SEQUENTIAL = 2;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestInitialization()
        {

            // if test fails see if MIN_SEQUENTIAL value was changed in Source.
            ushort seq = 10;
            Source s = new Source(seq);
            Assert.AreEqual(seq, s.BaseSequence);
            Assert.AreEqual(seq - 1, s.MaxSequence);
            Assert.AreEqual(RTPConstants.RTP_SEQ_MOD + 1, s.BadSequence);
            Assert.AreEqual(MIN_SEQUENTIAL, s.Probation);
            // add additional instantiation logic
        }
        [Test]
        public void TestPacketInSequence()
        {
            ushort seq = 5;
            Source s = new Source(5);

            s.UpdateSequence(6);
            Assert.AreEqual(6, s.MaxSequence);
            Assert.AreEqual(0, s.Recieved);
            Assert.AreEqual(MIN_SEQUENTIAL - 1, s.Probation);
            s.UpdateSequence(7);
            Assert.AreEqual(7, s.MaxSequence);
            Assert.AreEqual(1, s.Recieved);
            s.UpdateSequence(8);
            Assert.AreEqual(8, s.MaxSequence);
            Assert.AreEqual(2, s.Recieved);

        }

        [Test]
        public void TestCycleIteration()
        {
            ushort num = 0xFFFF;
            Source s = new Source(num);
            s.UpdateSequence(num);
            Assert.AreEqual(num, s.MaxSequence);
            s.UpdateSequence(0);
        }
    }
}