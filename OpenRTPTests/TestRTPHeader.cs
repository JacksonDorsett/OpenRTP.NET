using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RTP.Net;
namespace OpenRTPTests
{
    class TestRTPHeader
    {
        [Test]
        public void TestSerialize()
        {
            RTPHeader t = new RTPHeader(2, false, false, 1, false, 32, 64, 1, 124, new uint[] { 16 });
            var bts = t.Serialize();
            Assert.AreEqual(129, bts[0]);
            Assert.AreEqual(32, bts[1]);
            // Sequence number
            Assert.AreEqual(0, bts[2]);
            Assert.AreEqual(64, bts[3]);

        }
    }
}
