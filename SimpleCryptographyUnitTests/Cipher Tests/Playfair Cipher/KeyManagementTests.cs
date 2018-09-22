using NUnit.Framework;
using SimpleCryptography.Ciphers.Playfair_Cipher.Key_Management;

namespace SimpleCryptographyUnitTests.Cipher_Tests.Playfair_Cipher
{
    [TestFixture]
    public class KeyManagementTests
    {
        private static readonly IPlayfairKeyManagement KeyManagement = new PlayfairKeyManagement();

        #region IsValidCipherKey

        [Test]
        [TestCase("ZABCDEFGHIJKLMNOPRSTUVWXY")]
        [TestCase("GRENOBLIACDFHJKMPSTUVWXYZ")]
        public void IsValidCipherKey_ValidKey_IsTrue(string cipherKey)
        {
            Assert.IsTrue(KeyManagement.IsValidCipherKey(new PlayfairKey {Value = cipherKey}));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("ABC")]
        [TestCase("abc")]
        [TestCase("ABCDE")]
        [TestCase("ABCDEF")]
        [TestCase("ABCDEFGHIJKLMNOPRSTUVWXYZ")]
        [TestCase("ABCDEFGHijkLMNOPRSTUVWXYZ")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXY")]
        [TestCase("ZBCDEFGHIJKLMNOPRSTUVWXYA1")]
        [TestCase("233DEFGHIJKLMNOPRSTUVWXYZ")]
        public void IsValidCipherKey_InvalidKey_IsFalse(string cipherKey)
        {
            Assert.IsFalse(KeyManagement.IsValidCipherKey(new PlayfairKey {Value = cipherKey}));
        }

        #endregion
    }
}