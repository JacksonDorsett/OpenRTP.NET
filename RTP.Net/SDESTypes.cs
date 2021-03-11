using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net
{
    public enum SDESTypes
    {
        END,  //end of SDES list
        CNAME,//canonical name
        NAME, //user name
        EMAIL,//email address
        PHONE,//phone number
        LOC,  //geographic user location
        TOOL, //name of application or tool
        NOTE, //notice about the source
        PRIV  //privte extensions
    }
}
