using System;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SimpleCryptography.Ciphers.Playfair_Cipher;

namespace SimpleCryptographyUnitTests.Cipher_Tests.Playfair_Cipher
{   
    [TestFixture]
    public class PlayfairCipherTests
    {
        private static readonly IPlayfairCipher Cipher = new PlayfairCipher();

        [Test]
        public void Encrypt_InvalidParams_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Cipher.EncryptMessage(string.Empty, "Hello"));
            Assert.Throws<ArgumentNullException>(() => Cipher.EncryptMessage("Msg", string.Empty));
            Assert.Throws<ArgumentException>(() => Cipher.EncryptMessage("5412", "Hello"));
            Assert.Throws<ArgumentException>(() => Cipher.EncryptMessage("Valid msg", "8888888888"));
        }

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