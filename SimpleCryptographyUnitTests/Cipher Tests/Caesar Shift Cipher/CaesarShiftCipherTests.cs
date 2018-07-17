using NUnit.Framework;
using SimpleCryptography.Ciphers.Caesar_Shift_Cipher;

namespace SimpleCryptographyUnitTests.Cipher_Tests.Caesar_Shift_Cipher
{
    [TestFixture]
    public class CaesarShiftCipherTests
    {
        private static readonly CaesarShiftCipher Cipher = new CaesarShiftCipher();
        private const string EnglishAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string EngAlphabetMissingLetters = "ABCDFGHJKLMNOPRTUVWXYZ"; //I, E, Q, S missing

        [Test]
        [TestCase("Hi Dave!", EnglishAlphabet, 5, "MN IFAJ!")]
        [TestCase("I missed second question", EngAlphabetMissingLetters, 4, "I RISSEJ SEHUTJ QYESXIUT")]
        public void Encrypt_ValidInputs_AreEqual(string plainText, string alphabet, int shift, string expected)
        {
            Assert.AreEqual(expected, Cipher.EncryptMessage(plainText, alphabet, shift));
        }

        [Test]
        [TestCase("JHUJLS AOL MSPNOA", EnglishAlphabet, 7, "cancel the flight")]
        public void Decrypt_ValidInputs_AreEqual(string cipherText, string alphabet, int shift, string expected)
        {
            Assert.AreEqual(expected, Cipher.DecryptMessage(cipherText, alphabet, shift));
        }
    }
}