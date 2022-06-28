using NUnit.Framework;
using SimpleCryptoLib.Ciphers.Vigenere_Cipher;

namespace SimpleCryptoUnitTests.CipherTests.Vigenere_Square_Tests;

[TestFixture]
public class VigenereCipherTests
{
    private static readonly VigenereCipher Cipher = new VigenereCipher();
        
    [Test]
    [TestCase("", "HI")]
    [TestCase("Message to encrypt", "")]
    [TestCase("", "")]
    public void EncryptMessage_InvalidParams_EmptyString(string plainText, string memorableKey)
    {
        Assert.AreEqual(string.Empty, Cipher.EncryptMessage(plainText, new VigenereKey
        {
            MemorableKey = memorableKey
        }));
    }

    [Test]
    [TestCase("Call me", "NOSE", "PODP ZS")]
    [TestCase("thesunandthemaninthemoon", "KING", "DPRYEVNTNBUKWIAOXBUKWWBT")]
    public void EncryptMessage_ValidInput_AreEqual(string plainText, string memorableKey, string expected)
    {
        Assert.AreEqual(expected, Cipher.EncryptMessage(plainText, new VigenereKey
        {
            MemorableKey = memorableKey
        }));
    }
}