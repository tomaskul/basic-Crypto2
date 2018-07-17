using System;
using NUnit.Framework;
using SimpleCryptography.Ciphers.Vigenere_Cipher;

namespace SimpleCryptographyUnitTests.Cipher_Tests.Vigenere_Square
{
    [TestFixture]
    public class VigenereCipherTests
    {
        private static readonly VigenereCipher Cipher = new VigenereCipher();
        private const string EnglishAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        [Test]
        [TestCase("", EnglishAlphabet, "HI")]
        [TestCase("Message", "", "BILL")]
        [TestCase("Hide your gold", EnglishAlphabet, "")]
        public void EncryptMessage_InvalidParams_ThrowsArgumentNullException(string plainText, string alphabet, string key)
        {
            Assert.Throws<ArgumentNullException>(() => Cipher.EncryptMessage(plainText, alphabet, key));
        }

        [Test]
        [TestCase("Call me", "NOSE", "PODP ZS")]
        [TestCase("thesunandthemaninthemoon", "KING", "DPRYEVNTNBUKWIAOXBUKWWBT")]
        public void EncryptMessage_ValidParams_EncryptedMessage(string plainText, string key, string expected)
        {
            Assert.AreEqual(expected, Cipher.EncryptMessage(plainText, EnglishAlphabet, key));
        }
    }
}