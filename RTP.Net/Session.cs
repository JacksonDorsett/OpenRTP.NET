using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net
{
    class Session
    {
        int _currentTime; //the current time;
        int _tp; //the last time an RTCP packet was transmitted;
        int _tn; //the next scheduled transmission time of an RTCP packet;
        uint _prevMembers; //the estimated number of session members at the time tn was last recomputed;
        uint _members; //the most current estimate for the number of session members;
        uint _senders; // the most current estimate for the number of senders in the session;
        /// <summary>
        /// rtcp_bw: The target RTCP bandwidth, i.e., the total bandwidth
        ///that will be used for RTCP packets by all members of this session,
        ///in octets per second.This will be a specified fraction of the
        ///"session bandwidth" parameter supplied to the application at
        ///startup.
        /// </summary>
        uint _rtcp_bw;
        bool _we_sent; //Flag that is true if the application has sent data since the 2nd previous RTCP report was transmitted.
        /// <summary>
        /// avg_rtcp_size: The average compound RTCP packet size, in octets,
        ///over all RTCP packets sent and received by this participant.The
        ///size includes lower-layer transport and network protocol headers
        ///(e.g., UDP and IP) as explained in Section 6.2.
        /// </summary>
        uint _avg_rtcp_size;
        /// <summary>
        /// initial: Flag that is true if the application has not yet sent
        ///an RTCP packet.
        /// </summary>
        bool _initial;
        public Session()
        {
            this._tp = 0;
            this._senders = 0;
            this._prevMembers = 1;
            this._members = 1;
            this._we_sent = false;
            this._initial = true;

        }
    }
}
