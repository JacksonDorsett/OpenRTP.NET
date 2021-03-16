﻿using System;
using System.Collections.Generic;
using System.Text;
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
            var b = new RTCP_BYE(a);
            var t = b.Serialize();
            Assert.AreEqual(8, t.Length);
            Assert.AreEqual(18, t[1]);
            Assert.AreEqual(210, t[6]);
        }
    }
}