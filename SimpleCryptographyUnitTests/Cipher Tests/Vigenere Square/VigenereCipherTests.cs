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
        public void Encrypt_InvalidParams()
        {
            Assert.Throws<ArgumentNullException>(() => Cipher.EncryptMessage(string.Empty, EnglishAlphabet, "HI"));
            Assert.Throws<ArgumentNullException>(() => Cipher.EncryptMessage("Message", string.Empty, "HI"));
            Assert.Throws<ArgumentNullException>(() => Cipher.EncryptMessage("Message", EnglishAlphabet, string.Empty));
        }

        [Test]
        public void Encrypt_ShortMessage()
        {
            var encryptedMessage = Cipher.EncryptMessage("Call me", EnglishAlphabet, "NOSE");
            Assert.AreEqual("PODP ZS", encryptedMessage);
        }
    }
}