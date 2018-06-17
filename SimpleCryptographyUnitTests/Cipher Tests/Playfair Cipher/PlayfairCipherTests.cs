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

        [Test]
        public void QRegex_IsMatch()
        {
            const string pattern = @"[a-pr-zA-PR-Z]";
            Assert.IsFalse(Regex.IsMatch("QQ", pattern));
            Assert.IsFalse(Regex.IsMatch(" q", pattern));
            Assert.IsFalse(Regex.IsMatch("q  ", pattern));
            Assert.IsFalse(Regex.IsMatch(" q q   ", pattern));
            Assert.IsFalse(Regex.IsMatch("   ", pattern));
            Assert.IsTrue(Regex.IsMatch("albuquerque", pattern));

            var regex = new Regex(pattern);
            var matches = regex.Matches("albuquerque", 0);
            var sb = new StringBuilder(string.Empty);
            for (var i = 0; i < matches.Count; i++)
            {
                sb.Append(matches[i]);
            }
            
            
            Assert.AreEqual("albuuerue", sb.ToString());
        }
    }
}