using System;
using System.Security.Cryptography;
using System.Text;

namespace RTP.Net.Utils.IdentificationGeneration
{
    /// <summary>
    ///     Generates a random 32 bit identifier using
    ///     MD5 routines.
    /// </summary>
    public static class RandomIdentificationGenerator
    {
        /// <summary>
        ///     A pseudorandom number generator.
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        ///     Our method gets a random 32-bit identifier using
        ///     MD5.
        ///     (https://tools.ietf.org/html/rfc1321)
        ///     https://msdn.microsoft.com/en-us/library/system.security.cryptography.md5%28v=vs.110%29.aspx
        /// </summary>
        public static byte[] GetRandomIdentification(SDESType type)
        {
            // Creates a new instance of this class, gets random hashcodes
            var typeCode = type.GetHashCode();
            var dateTime = DateTime.Now.Ticks;

            // make it more random
            var inputString = (typeCode * dateTime + Random.Next(dateTime.GetHashCode()));

            using var md5 = MD5.Create();
            var input = Encoding.ASCII.GetBytes(inputString.ToString());
            var hash = md5.ComputeHash(input);

            return hash;
        }
    }
}