using System.Collections.Generic;
using NUnit.Framework;
using SimpleCryptoLib.Ciphers.Monoalphabetic_Substitution_Cipher;

namespace SimpleCryptoUnitTests.CipherTests.Monoalphabetic_Substitution_Cipher;

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
        [TestCase("Hi Dave!", "MT RCHS!")]
        public void Encrypt_ValidInput_Key_AreEqual(string plainText, string cipherText)
        {
            var encryptedMessage = Cipher.EncryptMessage(plainText, Key);
            
            Assert.AreEqual(cipherText, encryptedMessage);
        }

        [Test]
        [TestCase("Hi Dave!", "MTRCHS")]
        public void Encrypt_ValidInput_KeyNoPunctuation_AreEqual(string plainText, string cipherText)
        {
            var encryptedMessage = Cipher.EncryptMessage(plainText, KeyNoPunctuation);
            
            Assert.AreEqual(cipherText, encryptedMessage);
        }

        [Test]
        [TestCase("RKI'X LYEJTWM XMS LCWWGKDR!", "don't publish the password!")]
        public void Decrypt_ValidInput_Key_AreEqual(string cipherText, string plainText)
        {
            var decryptedMessage = Cipher.DecryptMessage(cipherText, Key);
            
            Assert.AreEqual(plainText, decryptedMessage);
        }

        [Test]
        [TestCase("RKIXLYEJTWMXMSLCWWGKDR", "dontpublishthepassword")]
        public void Decrypt_ValidInput_KeyNoPunctuation_AreEqual(string cipherText, string plainText)
        {
            var decryptedMessage = Cipher.DecryptMessage(cipherText, KeyNoPunctuation);
            
            Assert.AreEqual(plainText, decryptedMessage);
        }
}