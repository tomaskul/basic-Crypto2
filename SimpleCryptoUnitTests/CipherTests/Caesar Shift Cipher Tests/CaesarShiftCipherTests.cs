using System.Collections.Generic;
using NUnit.Framework;
using SimpleCryptoLib.Ciphers.Caesar_Shift_Cipher;

namespace SimpleCryptoUnitTests.CipherTests.Caesar_Shift_Cipher_Tests
{
    [TestFixture]
    public class CaesarShiftCipherTests : CommonCipherTestBase<CaesarShiftCipher, CaesarShiftCipherKey>
    {
        private const string EngAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string EngAlphabetMissingLetters = "ABCDFGHJKLMNOPRTUVWXYZ"; // E, I, Q, S omitted.

        protected override CaesarShiftCipher SystemUnderTest { get; set; } = new CaesarShiftCipher();

        protected override IEnumerable<(string plainText, CaesarShiftCipherKey key, string expected)> EncryptionValidationDataSet()
        {
            yield return (
                "Hi Dave!", 
                new CaesarShiftCipherKey{ Alphabet = EngAlphabet, Shift = 5 },
                "MN IFAJ!");
            
            yield return (
                "I missed second question",
                new CaesarShiftCipherKey { Alphabet = EngAlphabetMissingLetters, Shift = 4 },
                "I RISSEJ SEHUTJ QYESXIUT");
        }

        protected override IEnumerable<(string cipherText, CaesarShiftCipherKey key, string expected)> DecryptionValidationDataSet()
        {
            yield return (
                "JHUJLS AOL MSPNOA",
                new CaesarShiftCipherKey { Alphabet = EngAlphabet, Shift = 7 },
                "cancel the flight");
        }
    }
}