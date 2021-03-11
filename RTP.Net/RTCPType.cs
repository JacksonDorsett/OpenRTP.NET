using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net
{
    public enum RTCPType
    {
        SR = 200, //Sender Report
        RR = 201, //Reciever Report
        SDES = 202, //Source Description
        BYE = 203, //Goodbye
        APP = 204 //Application-Defined
    }
}
