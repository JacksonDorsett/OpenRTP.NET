using System;
using System.Collections.Generic;
using RTP.Net.Utils;

namespace RTP.Net.RTCP
{
    /// <summary>
    ///     Represents the 24 bit packet lost field
    /// </summary>
    public class PacketsLost : ISerialize
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PacketsLost" /> class.
        /// </summary>
        /// <param name="n">An unsigned integer for data packets.</param>
        public PacketsLost(int n)
        {
            if (n >= 1 << 24)
                throw new ArgumentOutOfRangeException("Packet Loss Exception: input must be less than 25 bytes");

            // A mask that clamps at packet loss.
            const int mask = 0x7FFFF;

            // Check if the unsigned integer is positive or negative
            if (((1 << 23) & n) == 1)
            {
                this._isPositive = false;

                // clamp at the mask
                this._numberOfPacketsLost = n | mask;
            }
            else
            {
                _isPositive = true;
                this._numberOfPacketsLost = n;
            }
        }

        /// <summary>
        ///     Gets the boolean representing the positivity of this mask.
        /// </summary>
        private readonly bool _isPositive;

        /// <summary>
        ///     The number of packets lost.
        /// </summary>
        private readonly int _numberOfPacketsLost;

        /// <summary>
        ///     Gets the number of packets lost.
        /// </summary>
        public int Lost => (_isPositive) ? _numberOfPacketsLost : -_numberOfPacketsLost;

        /// <summary>
        ///     Serializes the number of packets lost.
        /// </summary>
        /// <returns>The serialization of the network.</returns>
        public byte[] Serialize()
        {
            var l = new List<byte>(NetworkSerializer.Serialize((uint) Lost));
            l.RemoveAt(0);
            return l.ToArray();
        }
    }
}