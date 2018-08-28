using NUnit.Framework;
using SimpleCryptography.Ciphers.Playfair_Cipher.Key_Management;

namespace SimpleCryptographyUnitTests.Cipher_Tests.Playfair_Cipher
{
    [TestFixture]
    public class KeyManagementTests
    {
        private static readonly IPlayfairKeyManagement KeyManagement 
            = new PlayfairKeyManagement("ABCDEFGHIJKLMNOPRSTUVWXYZ", 'Q');
        
        #region IsValidCipherKey

        [Test]
        [TestCaseSource(nameof(ValidPlayfairCipherKeys))]
        public void IsValidCipherKey_ValidKey_IsTrue(char[,] cipherKey)
        {
            Assert.IsTrue(KeyManagement.IsValidCipherKey(cipherKey));
        }
        
        [Test]
        [TestCaseSource(nameof(InvalidPlayfairCipherKeys))]
        public void IsValidCipherKey_InvalidKey_IsFalse(char[,] cipherKey)
        {
            Assert.IsFalse(KeyManagement.IsValidCipherKey(cipherKey));
        }
        
        private static readonly char[][,] ValidPlayfairCipherKeys =
        {
            new char[5,5]
            {
                {'Z', 'A', 'B', 'C', 'D'},
                {'E', 'F', 'G', 'H', 'I'},
                {'J', 'K', 'L', 'M', 'N'},
                {'O', 'P', 'R', 'S', 'T'},
                {'U', 'V', 'W', 'X', 'Y'}
            },
            new char[5,5]
            {
                {'G', 'R', 'E', 'N', 'O'},
                {'B', 'L', 'I', 'A', 'C'},
                {'D', 'F', 'H', 'J', 'K'},
                {'M', 'P', 'S', 'T', 'U'},
                {'V', 'W', 'X', 'Y', 'Z'}
            }, 
        };

        private static readonly char[][,] InvalidPlayfairCipherKeys =
        {
            new char[,]
            {
                {'A', 'B', 'C'}
            },
            new char[,]
            {
                {'A', 'B', 'C', 'D', 'E'}
            },
            new char[,]
            {
                {'A', 'B', 'C', 'D', 'E', 'F'}
            },
            new char[2,3]
            {
                {'A', 'B', 'C'},
                {'D', 'E', 'F'}
            },
            new char[5,5]
            {
                {'A', 'B', 'C', 'D', 'E'},
                {'F', 'G', 'H', 'I', 'J'},
                {'K', 'L', 'M', 'N', 'O'},
                {'P', 'R', 'S', 'T', 'U'},
                {'V', 'W', 'X', 'Y', 'Z'}
            },
            new char[5,5]
            {
                {'A', 'B', 'C', 'D', 'E'},
                {'F', 'G', 'H', 'I', 'J'},
                {'K', 'L', 'Q', 'N', 'O'},
                {'P', 'R', 'S', 'T', 'U'},
                {'V', 'W', 'X', 'Y', 'Z'}
            }
        };

        #endregion
    }
}