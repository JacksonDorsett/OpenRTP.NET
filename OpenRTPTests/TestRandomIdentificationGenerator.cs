using NUnit.Framework;
using RTP.Net;
using RTP.Net.Utils.IdentificationGeneration;

namespace OpenRTPTests
{
    /// <summary>
    ///     NUnit tests for testing that the random identification
    ///     generator actually works.
    /// </summary>
    [TestFixture]
    public class TestRandomIdentificationGenerator
    {
        [TestCase(SDESType.NAME, SDESType.NAME, ExpectedResult = false)]
        [TestCase(SDESType.CNAME, SDESType.EMAIL, ExpectedResult = false)]
        [TestCase(SDESType.EMAIL, SDESType.PRIV, ExpectedResult = false)]
        [TestCase(SDESType.NOTE, SDESType.TOOL, ExpectedResult = false)]
        [TestCase(SDESType.PHONE, SDESType.NAME, ExpectedResult = false)]
        [TestCase(SDESType.LOC, SDESType.CNAME, ExpectedResult = false)]
        public bool TestIdentificationGenerator(SDESType one, SDESType two)
        {
            var identificationOne = RandomIdentificationGenerator.GetRandomIdentification(one);
            var identificationTwo = RandomIdentificationGenerator.GetRandomIdentification(two);
            
            return IsEqual(identificationOne, identificationTwo);
        }

        /// <summary>
        ///     Checks if two byte arrays are equal.
        /// </summary>
        /// <param name="byteCollectionOne">Our first byte collection.</param>
        /// <param name="byteCollectionTwo">Our second byte collection.</param>
        /// <returns>A boolean representing the equality of these two byte arrays.</returns>
        private static bool IsEqual(byte[] byteCollectionOne, byte[] byteCollectionTwo)
        {
            if (byteCollectionOne.Length != byteCollectionTwo.Length)
            {
                return false;
            }

            // At this point, both collections are equal in length so we can
            // simply choose one
            var index = 0;
            while (index < byteCollectionOne.Length)
            {
                if (byteCollectionOne[index] != byteCollectionTwo[index])
                {
                    return false;
                }

                // increment the index
                index += 1;
            }

            // well, since every byte is equal we conclude that these two sets are equal
            return true;
        }
    }
}
