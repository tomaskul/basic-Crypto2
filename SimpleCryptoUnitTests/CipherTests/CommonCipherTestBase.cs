using System.Collections.Generic;
using NUnit.Framework;
using SimpleCryptoLib.Ciphers.Common;
using SimpleCryptoLib.Ciphers.Common.Key_Management;

namespace SimpleCryptoUnitTests.CipherTests;

[TestFixture]
public abstract class CommonCipherTestBase<TCipher, TKey> where TCipher : ICommonCipher<TKey> where TKey : CipherKeyBase
{
    /// <summary>The system under test, injected via constructor.</summary>
    protected abstract TCipher SystemUnderTest { get; set; }
    
    /// <summary>Valid encryption test data.</summary>
    /// <returns>
    /// Collection of tuples, where each tuple has plain text to be encrypted, encryption key and the expected result.
    /// </returns>
    protected abstract IEnumerable<(string plainText, TKey key, string expected)> EncryptionValidationDataSet();
    
    /// <summary>Valid decryption test data.</summary>
    /// <returns>
    /// Collection of tuples, where each tuple has cipher text to be decrypted, encryption key and the expected result.
    /// </returns>
    protected abstract IEnumerable<(string cipherText, TKey key, string expected)> DecryptionValidationDataSet();

    [Test]
    public void EncryptMessage_ValidInput_AreEqual()
    {
        foreach (var (plainText, key, expected) in EncryptionValidationDataSet())
        {
            Assert.AreEqual(expected, SystemUnderTest.EncryptMessage(plainText, key));   
        }
    }

    [Test]
    public void DecryptMessage_ValidInput_AreEqual()
    {
        foreach (var (cipherText, key, expected) in DecryptionValidationDataSet())
        {
            Assert.AreEqual(expected, SystemUnderTest.DecryptMessage(cipherText, key));
        }
    }
}