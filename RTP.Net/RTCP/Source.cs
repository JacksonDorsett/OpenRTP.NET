using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net.RTCP
{
    public class Source
    {
        public Source(ushort seq)
        {
            InitSequence(seq);
        }


        /// <summary>
        /// highest seq. number seen
        /// </summary>
        public ushort MaxSequence { get; private set; }

        /// <summary>
        /// shifted count of seq. number cycles
        /// </summary>
        public uint Cycles { get; private set; }

        /// <summary>
        /// base seq number
        /// </summary>
        public uint BaseSequence { get; private set; }

        /// <summary>
        /// last 'bad' seq number + 1 
        /// </summary>
        public uint BadSequence { get; private set; }

        /// <summary>
        /// sequ. packets till source is valid
        /// </summary>
        public uint Probation { get; private set; }

        /// <summary>
        /// packets received
        /// </summary>
        public uint Recieved { get; private set; }

        /// <summary>
        /// packet expected at last interval
        /// </summary>
        public uint ExpectedPrior { get; private set; }

        /// <summary>
        /// packet received at last interval
        /// </summary>
        public uint RecievedPrior { get; private set; }

        /// <summary>
        /// relative trans time for prev pkt
        /// </summary>
        public uint Transmit { get; private set; }

        /// <summary>
        /// estimated jitter
        /// </summary>
        public uint Jitter { get; private set; }
    
        public int UpdateSequence(ushort seq)
        {
            /// Replace return code with exception??
            ushort udelta = (ushort)(seq - MaxSequence);
            const int MAX_DROPOUT = 3000;
            const int MAX_DISORDER = 100;
            const int MIN_Sequential = 2;

            /*
             * Source is not valid until MIN_SEQUENTIAL packets with
             * sequential sequence numbers have been received.
             */
            if (this.Probation != 0)
            {
                // Packet is in sequence
                if (seq == (MaxSequence + 1))
                {
                    Probation--;
                    MaxSequence = seq;
                    if(Probation == 0)
                    {
                        InitSequence(seq);
                    }
                    Recieved++;
                    return 1;
                }
                else
                {
                    Probation = MIN_Sequential - 1;
                    MaxSequence = seq;
                }
                return 0;
            }
            else if (udelta < MAX_DROPOUT)
            {
                //in order with permissible gap.
                if (seq < MaxSequence)
                {
                    //Sequence Number wrapped - count another 64k cycle.
                    Cycles += RTPConstants.RTP_SEQ_MOD;
                }
                MaxSequence = seq;
            }
            else if (udelta <= RTPConstants.RTP_SEQ_MOD - MAX_DISORDER)
            {
                //the sequence made a very large jump
                if (seq == BadSequence)
                {
                    /*
                    * Two sequential packets -- assume that the other side
                    * restarted without telling us so just re-sync
                    * (i.e., pretend this was the first packet).
                    */
                    InitSequence(seq);
                }
                else
                {
                    BadSequence = (uint)((seq + 1) & (RTPConstants.RTP_SEQ_MOD - 1));
                    return 0;
                }
            }
            else
            {
                // duplicate or reorder packet
                // Implement later
            }
            Recieved++;
            return 1;

        }

        private void InitSequence(ushort seq)
        {
            this.BaseSequence = seq;
            this.MaxSequence = seq;
            this.BadSequence = RTPConstants.RTP_SEQ_MOD + 1;
            this.Cycles = 0;
            this.Recieved = 0;
            this.RecievedPrior = 0;
            this.ExpectedPrior = 0;
        }
    }
}
