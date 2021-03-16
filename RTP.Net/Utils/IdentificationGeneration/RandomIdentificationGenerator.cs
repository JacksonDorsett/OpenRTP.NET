using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

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
        public static byte[] GetRandomIdentification(SDESType type = SDESType.CNAME)
        {
            // Creates a new instance of this class, gets random hashcodes
            var typeCode = type.GetHashCode();
            var dateTime = DateTime.Now.Ticks;

            // make it more random
            var inputString = (typeCode * Random.Next(int.MaxValue));

            // use the C# library to do the heavy lifting
            using var md5 = MD5.Create();
            var input = Encoding.ASCII.GetBytes(inputString.ToString());
            var hash = md5.ComputeHash(input, 0, count: 4);

            return hash;
        }
    }
}