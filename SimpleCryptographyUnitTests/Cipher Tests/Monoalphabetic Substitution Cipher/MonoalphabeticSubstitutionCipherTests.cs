using System.Collections.Generic;
using NUnit.Framework;
using SimpleCryptography.Ciphers.Monoalphabetic_Substitution_Cipher;

namespace SimpleCryptographyUnitTests.Cipher_Tests.Monoalphabetic_Substitution_Cipher
{
    [TestFixture]
    public class MonoalphabeticSubstitutionCipherTests
    {
        private static readonly MonoalphabeticSubstitutionCipher Cipher = new MonoalphabeticSubstitutionCipher();
        private static readonly MonoalphabeticSubstitutionKey Key = new MonoalphabeticSubstitutionKey(new Dictionary<char, char>()
        {
            { 'm', 'A' }, { 'g', 'B' }, { 'a', 'C' }, { 'r', 'D' }, { 'b', 'E' },
            { 'q', 'F' }, { 'w', 'G' }, { 'v', 'H' }, { 'n', 'I' }, { 'l', 'J' },
            { 'o', 'K' }, { 'p', 'L' }, { 'h', 'M' }, { 'j', 'N' }, { 'f', 'O' },
            { 'x', 'P' }, { 'c', 'Q' }, { 'd', 'R' }, { 'e', 'S' }, { 'i', 'T' },
            { 'k', 'U' }, { 'y', 'V' }, { 's', 'W' }, { 't', 'X' }, { 'u', 'Y' },
            { 'z', 'Z' }, { ' ', ' ' }, { '.', '.' }, { '\'','\'' }, { ',', ',' },
            { ';', ';' }, { '!', '!' }
        });
        private static readonly MonoalphabeticSubstitutionKey KeyNoPunctuation = new MonoalphabeticSubstitutionKey(new Dictionary<char, char>()
        {
            { 'm', 'A'}, { 'g', 'B'}, { 'a', 'C'}, { 'r', 'D'}, { 'b', 'E'},
            { 'q', 'F'}, { 'w', 'G'}, { 'v', 'H'}, { 'n', 'I'}, { 'l', 'J'},
            { 'o', 'K'}, { 'p', 'L'}, { 'h', 'M'}, { 'j', 'N'}, { 'f', 'O'},
            { 'x', 'P'}, { 'c', 'Q'}, { 'd', 'R'}, { 'e', 'S'}, { 'i', 'T'},
            { 'k', 'U'}, { 'y', 'V'}, { 's', 'W'}, { 't', 'X'}, { 'u', 'Y'},
            { 'z', 'Z'}
        });

        [Test]
        public void Encrypt()
        {
            var encryptedMessage = Cipher.EncryptMessage("Hi Dave!", Key);
            
            Assert.AreEqual("MT RCHS!", encryptedMessage);
        }

        [Test]
        public void Encrypt_NoPunctuation()
        {
            var encryptedMessage = Cipher.EncryptMessage("Hi Dave!", KeyNoPunctuation);
            
            Assert.AreEqual("MTRCHS", encryptedMessage);
        }

        [Test]
        public void Decrypt()
        {
            var decryptedMessage = Cipher.DecryptMessage("RKI'X LYEJTWM XMS LCWWGKDR!", Key);
            
            Assert.AreEqual("don't publish the password!", decryptedMessage);
        }

        [Test]
        public void DecryptNoPunctuation()
        {
            var decryptedMessage = Cipher.DecryptMessage("RKIXLYEJTWMXMSLCWWGKDR", KeyNoPunctuation);
            
            Assert.AreEqual("dontpublishthepassword", decryptedMessage);
        }
    }
}