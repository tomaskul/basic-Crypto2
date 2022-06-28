using SimpleCryptoLib.Ciphers.Common.Key_Management;

namespace SimpleCryptoLib.Ciphers.Vigenere_Cipher;

/// <summary>The Vigenere cipher key.</summary>
public class VigenereKey : CipherKeyBase
{
    /// <summary>The memorable key.</summary>
    public string MemorableKey { get; set; }
}