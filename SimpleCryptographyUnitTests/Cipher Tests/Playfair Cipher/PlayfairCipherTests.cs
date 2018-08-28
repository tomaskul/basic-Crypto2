using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SimpleCryptography.Ciphers.Playfair_Cipher;

namespace SimpleCryptographyUnitTests.Cipher_Tests.Playfair_Cipher
{
    [TestFixture]
    public class PlayfairCipherTests
    {
        private static readonly IDigramGenerator DigramGenerator = new DigramGenerator('X');
        private static readonly IPlayfairCipher Cipher = new PlayfairCipher(DigramGenerator);

        [Test]
        [TestCase("", "Hello")]
        [TestCase("Msg", "")]
        public void Encrypt_InvalidParams_ThrowsArgumentNullException(string message, string key)
        {
            Assert.Throws<ArgumentNullException>(() => Cipher.EncryptMessage(message, key));
        }

        [Test]
        [TestCase("5412", "Hello")]
        [TestCase("Valid msg", "88888888")]
        public void Encrypt_InvalidParams_ThrowsArgumentException(string message, string key)
        {
            Assert.Throws<ArgumentException>(() => Cipher.EncryptMessage(message, key));
        }

        [Test]
        public void Encrypt_InvalidKey_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => Cipher.EncryptMessage("msg", "abcdefghijklmnoprstuvwxyz"));
        }

        #region IsValidCipherKey

        [Test]
        [TestCaseSource(nameof(ValidPlayfairCipherKeys))]
        public void IsValidCipherKey_ValidKey_IsTrue(char[,] cipherKey)
        {
            Assert.IsTrue(Cipher.IsValidCipherKey(cipherKey));
        }
        
        [Test]
        [TestCaseSource(nameof(InvalidPlayfairCipherKeys))]
        public void IsValidCipherKey_InvalidKey_IsFalse(char[,] cipherKey)
        {
            Assert.IsFalse(Cipher.IsValidCipherKey(cipherKey));
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

        #region Regex tests

        private const string _acceptablePattern = "[a-pr-zA-PR-Z]";

        [Test]
        [TestCase("QQ")]
        [TestCase("q")]
        [TestCase(" q q QQq")]
        [TestCase(" -  ")]
        [TestCase("10")]
        public void DoNotMatchPattern(string input)
        {
            Assert.IsFalse(Regex.IsMatch(input, _acceptablePattern));
        }

        [Test]
        [TestCase("zQQ")]
        [TestCase("qw")]
        [TestCase(" q q QQeq")]
        [TestCase(" -  m")]
        [TestCase("10s")]
        [TestCase("albuquerque")]
        public void DoMatchPattern(string input)
        {
            Assert.IsTrue(Regex.IsMatch(input, _acceptablePattern));
        }

        [Test]
        [TestCase("zQQ", "z")]
        [TestCase("qw", "w")]
        [TestCase(" q q QQeq", "e")]
        [TestCase(" -  m", "m")]
        [TestCase("10s", "s")]
        [TestCase("albuquerque", "albuuerue")]
        [TestCase("abcdefghijklmnopqrstuvwxyz", "abcdefghijklmnoprstuvwxyz")]
        public void RegexPatternMatch(string input, string expected)
        {
            var regex = new Regex(_acceptablePattern);
            var matches = regex.Matches(input, 0);

            var sb = new StringBuilder(string.Empty);
            for (var i = 0; i < matches.Count; i++)
            {
                sb.Append(matches[i]);
            }

            Assert.AreEqual(expected, sb.ToString());
        }

        #endregion
    }
}