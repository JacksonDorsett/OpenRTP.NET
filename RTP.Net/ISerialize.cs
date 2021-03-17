using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net
{
    internal interface ISerialize
    {
        byte[] Serialize();
    }
}
