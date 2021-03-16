﻿using System;
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
            RTPHeader t = new RTPHeader(RTPVersion.TWO, false, false, 1, false, 32, 64, 1, 1240000, new uint[] { 16 });
            var bts = t.Serialize();
            Assert.AreEqual(129, bts[0]);
            Assert.AreEqual(32, bts[1]);
            // Sequence number
            Assert.AreEqual(0, bts[2]);
            Assert.AreEqual(64, bts[3]);
            // timestamp
            Assert.AreEqual(0, bts[4]);
            Assert.AreEqual(1, bts[7]);
            // SSRC
            Assert.AreEqual(0, bts[8]);
            Assert.AreEqual(18, bts[9]);
            Assert.AreEqual(235, bts[10]);
            Assert.AreEqual(192, bts[11]);

        }
    }
}