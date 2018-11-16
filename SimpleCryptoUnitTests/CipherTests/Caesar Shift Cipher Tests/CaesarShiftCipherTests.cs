using NUnit.Framework;
using SimpleCryptoLib.Ciphers.Caesar_Shift_Cipher;

namespace SimpleCryptoUnitTests.CipherTests.Caesar_Shift_Cipher_Tests
{
    [TestFixture]
    public class CaesarShiftCipherTests
    {
        private static readonly ICaesarShiftCipher Cipher = new CaesarShiftCipher();
        private const string EngAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string EngAlphabetMissingLetters = "ABCDFGHJKLMNOPRTUVWXYZ"; // E, I, Q, S omitted.

        [Test]
        [TestCase("Hi Dave!", EngAlphabet, 5, "MN IFAJ!")]
        [TestCase("I missed second question", EngAlphabetMissingLetters, 4, "I RISSEJ SEHUTJ QYESXIUT")]
        public void EncryptMessage_ValidInputs_AreEqual(string plainText, string alphabet, int shift, string expected)
        {
            Assert.AreEqual(expected, Cipher.EncryptMessage(plainText, new CaesarShiftCipherKey
            {
                Alphabet = alphabet,
                Shift = shift
            }));
        }

        [Test]
        [TestCase("JHUJLS AOL MSPNOA", EngAlphabet, 7, "cancel the flight")]
        public void DecryptMessage_ValidInputs_AreEqual(string cipherText, string alphabet, int shift, string expected)
        {
            Assert.AreEqual(expected, Cipher.DecryptMessage(cipherText, new CaesarShiftCipherKey
            {
                Alphabet = alphabet,
                Shift = shift
            }));
        }
    }
}