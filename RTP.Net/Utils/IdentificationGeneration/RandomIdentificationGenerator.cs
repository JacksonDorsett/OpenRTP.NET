using System;
using System.Diagnostics;
using System.IO;
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
        ///     The number of bytes in our array.
        /// </summary>
        private const int NumBytes = 4;

        /// <summary>
        ///     Our method gets a random 32-bit identifier using
        ///     MD5.
        ///     (https://tools.ietf.org/html/rfc1321)
        ///     https://msdn.microsoft.com/en-us/library/system.security.cryptography.md5%28v=vs.110%29.aspx
        /// </summary>
        public static byte[] GetRandomIdentification(SDESType type = SDESType.CNAME)
        {
            // Basically obtains various sources of randomness 
            var randomNumber = Random.Next(int.MaxValue);
            var processID = Process.GetCurrentProcess().ToString();
            var dateTime = DateTime.Now.ToString("h:mm:ss tt zz");
            var typeCode = type.GetHashCode();


            // a binary writer just for Jackson Dorsett :)
            using var memoryStream = new MemoryStream();
            using var binaryWriter = new BinaryWriter(memoryStream);
            binaryWriter.Write(randomNumber);
            binaryWriter.Write(processID);
            binaryWriter.Write(dateTime);
            binaryWriter.Write(typeCode);

            // use the C# library to do the heavy lifting
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(memoryStream.ToArray(), 0, count: 4);

            // closing the streams
            memoryStream.Close();
            binaryWriter.Close();

            // return our unique identifier :-)
            return hash;
        }
    }
}