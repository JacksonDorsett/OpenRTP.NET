using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RTP.Net.RTCP;
namespace OpenRTPTests
{
    class BYETest
    {
        [Test]
        public void TestByeSerialization()
        {
            uint[] a = { 1211515, 512534 };
            var b = new RTCP_BYE(a);
            var t = b.Serialize();
        }
    }
}
