using NUnit.Framework;
using SimpleCryptoLib.Ciphers.Playfair_Cipher;

namespace SimpleCryptoUnitTests.CipherTests.Playfair_Cipher;

[TestFixture]
public class PlayfairUtilTests
{
    [Test]
    [TestCase("BM")]
    [TestCase("BMNDZBXDKYBEJVDMUIXMMNUVIF")]
    public void IsCipherTextValid_ValidCipherText_IsTrue(string cipherText)
    {
        Assert.IsTrue(PlayfairUtil.IsValidCipherText(cipherText));
    }

    [Test]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("a")]
    [TestCase("Bm")]
    [TestCase("ZB.")]
    [TestCase("BMNDZBX")]
    public void IsCipherTextValid_InvalidCipherText_IsFalse(string cipherText)
    {
        Assert.IsFalse(PlayfairUtil.IsValidCipherText(cipherText));
    }
}