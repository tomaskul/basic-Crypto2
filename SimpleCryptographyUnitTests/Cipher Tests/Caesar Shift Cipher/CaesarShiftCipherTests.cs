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
        public void Encrypt_Shift_5_CorrectAlphabet()
        {
            var encryptedMessage = Cipher.EncryptMessage("Hi Dave!", EnglishAlphabet, 5);
            
            Assert.AreEqual("MN IFAJ!", encryptedMessage);
        }

        [Test]
        public void Decrypt_Shift_7_CorrectAlphabet()
        {
            var decryptedMessage = Cipher.DecryptMessage("JHUJLS AOL MSPNOA", EnglishAlphabet, 7);
            
            Assert.AreEqual("cancel the flight", decryptedMessage);
        }

        [Test]
        public void Encrypt_Shift_4_BrokenKey()
        {
            var encryptedMessage = Cipher.EncryptMessage("I missed second question", EngAlphabetMissingLetters, 4);
            
            Assert.AreEqual("I RISSEJ SEHUTJ QYESXIUT", encryptedMessage);
        }
    }
}