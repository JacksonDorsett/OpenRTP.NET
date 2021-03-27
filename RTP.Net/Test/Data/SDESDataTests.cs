using System;
using System.Collections.Generic;
using System.Text;
using RTP.Net.RTCP;
using NUnit.Framework;

namespace RTP.Net.Test.Data
{
    class SDESDataTests
    {
        [Test]
        public void TestSdesSerialize()
        {
            SDESItem[] a = { new SDESItem(2, 5, "Hello") };
            RTCP_SDES s = new RTCP_SDES(false, 3, 20, 1000, a);
            byte[] v = s.Serialize();
            //Test SSRC
            Assert.AreEqual(11, v.Length);
            Assert.AreEqual(3, v[2]);
            Assert.AreEqual(232, v[3]);
            Assert.AreEqual(2, v[4]);
            Assert.AreEqual(5, v[5]);
            Assert.AreEqual(111, v[10]);
        }
    }
}
