using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SimpleCryptoLib.Ciphers.Playfair_Cipher;
using SimpleCryptoLib.Ciphers.Playfair_Cipher.Digraphs;
using SimpleCryptoLib.Ciphers.Playfair_Cipher.Key_Management;

namespace SimpleCryptoUnitTests.CipherTests.Playfair_Cipher;

[TestFixture]
public class PlayfairCipherTests : CommonCipherTestBase<PlayfairCipher, PlayfairKey>
{
    protected override PlayfairCipher SystemUnderTest { get; set; } =
        new PlayfairCipher(new DigrathGenerator('X'), new PlayfairKeyManagement());

    [Test]
    [TestCase("5412", "Hello")]
    [TestCase("Valid msg", "88888888")]
    [TestCase("", "Hello")]
    [TestCase("Msg", "")]
    public void Encrypt_InvalidParams_ThrowsArgumentException(string message, string key)
    {
        Assert.Throws<ArgumentException>(() => SystemUnderTest.EncryptMessage(message, new PlayfairKey() { Value = key }));
    }

    [Test]
    public void Encrypt_InvalidKey_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            SystemUnderTest.EncryptMessage("msg", new PlayfairKey() { Value = "abcdefghijklmnoprstuvwxyz" }));
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

    protected override IEnumerable<(string plainText, PlayfairKey key, string expected)> EncryptionValidationDataSet()
    {
        yield return ("Hide the gold in the tree stump", new PlayfairKey { Value = "PLAYFIREXMBCDGHJKNOSTUVWZ" },
            "BMNDZBXDKYBEJVDMUIXMMNUVIF");
    }

    protected override IEnumerable<(string cipherText, PlayfairKey key, string expected)> DecryptionValidationDataSet()
    {
        yield return ("BMNDZBXDKYBEJVDMUIXMMNUVIF", new PlayfairKey { Value = "PLAYFIREXMBCDGHJKNOSTUVWZ" },
            "hidethegoldinthetrexestump");
    }
}