using System;
using System.Collections.Generic;
using System.Text;

namespace RTP.Net.RTCP
{
    class Source
    {
        public Source(ushort seq)
        {
            this.BaseSequence = seq;
            this.MaxSequence = seq;
            this.BadSequence = RTPConstants.RTP_SEQ_MOD + 1;
            this.Cycles = 0;
            this.Recieved = 0;
            this.RecievedPrior = 0;
            this.ExpectedPrior = 0;
        }
        public Source(ushort maxSequence, uint cycles, uint baseSequence, uint badSequence, uint probation, uint recieved, uint expectedPrior, uint recievedPrior, uint transmit, uint jitter)
        {
            MaxSequence = maxSequence;
            Cycles = cycles;
            BaseSequence = baseSequence;
            BadSequence = badSequence;
            Probation = probation;
            Recieved = recieved;
            ExpectedPrior = expectedPrior;
            RecievedPrior = recievedPrior;
            Transmit = transmit;
            Jitter = jitter;
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
    }
}
