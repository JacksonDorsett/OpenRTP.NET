using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace RTP.Net.Utils
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
        private static readonly RNGCryptoServiceProvider Random = new RNGCryptoServiceProvider();

        /// <summary>
        ///     The number of bytes that we want to use.
        /// </summary>
        private const short ByteArraySize = 16;

        /// <summary>
        ///     Our method gets a random 32-bit identifier using
        ///     MD5.
        ///     (https://tools.ietf.org/html/rfc1321)
        ///     https://msdn.microsoft.com/en-us/library/system.security.cryptography.md5%28v=vs.110%29.aspx
        /// </summary>
        public static byte[] GetRandomIdentification()
        {
            // an array of bytes
            var randomByteArray = new byte[ByteArraySize];

            // fills the byte array with cryptographically random values
            Random.GetBytes(randomByteArray);

            // use the C# library to do the heavy lifting
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(randomByteArray, 0, count: 4);

            // return our unique identifier :-)
            return hash;
        }
    }
}