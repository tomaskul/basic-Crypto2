using NUnit.Framework;
using SimpleCryptography.Ciphers.Vigenere_Cipher;

namespace SimpleCryptographyUnitTests.Cipher_Tests.Vigenere_Square
{
    [TestFixture]
    public class VigenereCipherTests
    {
        private static readonly VigenereCipher Cipher = new VigenereCipher();
        
        [Test]
        [TestCase("", "HI")]
        [TestCase("Message to encrypt", "")]
        [TestCase("", "")]
        public void EncryptMessage_InvalidParams_EmptyString(string plainText, string memorableKey)
        {
            Assert.AreEqual(string.Empty, Cipher.EncryptMessage(plainText, new VigenereKey
            {
                MemorableKey = memorableKey
            }));
        }

        [Test]
        [TestCase("Call me", "NOSE", "PODP ZS")]
        [TestCase("thesunandthemaninthemoon", "KING", "DPRYEVNTNBUKWIAOXBUKWWBT")]
        public void EncryptMessage_ValidInput_AreEqual(string plainText, string memorableKey, string expected)
        {
            Assert.AreEqual(expected, Cipher.EncryptMessage(plainText, new VigenereKey
            {
                MemorableKey = memorableKey
            }));
        }
    }
}