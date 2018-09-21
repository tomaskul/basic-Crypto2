using NUnit.Framework;
using SimpleCryptography.Ciphers.Playfair_Cipher;

namespace SimpleCryptographyUnitTests.Cipher_Tests.Playfair_Cipher
{
    [TestFixture]
    public class PlayfairUtilTests
    {
        [Test]
        [TestCase("BM")]
        [TestCase("BMNDZBXDKYBEJVDMUIXMMNUVIF")]
        public void IsValidCipherText_ValidCipherText_IsTrue(string cipherText)
        {
            Assert.IsTrue(PlayfairUtil.IsValidCipherText(cipherText));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("a")]
        [TestCase("Bm")]
        [TestCase("ZB.")]
        [TestCase("BMNDZBX")]
        public void IsValidCipherText_InvalidCipherText_IsFalse(string cipherText)
        {
            Assert.IsFalse(PlayfairUtil.IsValidCipherText(cipherText));
        }
    }
}