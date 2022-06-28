using System;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SimpleCryptoLib.Ciphers.Playfair_Cipher;
using SimpleCryptoLib.Ciphers.Playfair_Cipher.Digraphs;
using SimpleCryptoLib.Ciphers.Playfair_Cipher.Key_Management;

namespace SimpleCryptoUnitTests.CipherTests.Playfair_Cipher;

[TestFixture]
public class PlayfairCipherTests
{
    private static readonly IDigrathGenerator DigrathGenerator = new DigrathGenerator('X');
        private static readonly IPlayfairKeyManagement KeyManagement = new PlayfairKeyManagement();
        private static readonly IPlayfairCipher Cipher = new PlayfairCipher(DigrathGenerator, KeyManagement);

        [Test]
        [TestCase("5412", "Hello")]
        [TestCase("Valid msg", "88888888")]
        [TestCase("", "Hello")]
        [TestCase("Msg", "")]
        public void Encrypt_InvalidParams_ThrowsArgumentException(string message, string key)
        {
            Assert.Throws<ArgumentException>(() => Cipher.EncryptMessage(message, new PlayfairKey(){ Value = key }));
        }

        [Test]
        public void Encrypt_InvalidKey_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Cipher.EncryptMessage("msg", new PlayfairKey(){ Value = "abcdefghijklmnoprstuvwxyz" }));
        }

        [Test]
        [TestCase("Hide the gold in the tree stump", "PLAYFIREXMBCDGHJKNOSTUVWZ", "BMNDZBXDKYBEJVDMUIXMMNUVIF")]
        public void Encrypt_IsExpectedCipherText_AreEqual(string plainText, string cipherKey, string expectedCipherText)
        {
            Assert.AreEqual(expectedCipherText, Cipher.EncryptMessage(plainText, new PlayfairKey
            {
                Value = cipherKey
            }));
        }

        [Test]
        [TestCase("BMNDZBXDKYBEJVDMUIXMMNUVIF", "PLAYFIREXMBCDGHJKNOSTUVWZ", "hidethegoldinthetrexestump")]
        public void Decrypt_IsExpectedPlainText_AreEqual(string cipherText, string cipherKey, string expectedPlainText)
        {
            Assert.AreEqual(expectedPlainText, Cipher.DecryptMessage(cipherText, new PlayfairKey
            {
                Value = cipherKey
            }));
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