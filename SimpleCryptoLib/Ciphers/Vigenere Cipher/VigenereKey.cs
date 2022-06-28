using SimpleCryptoLib.Ciphers.Common.Key_Management;

namespace SimpleCryptoLib.Ciphers.Vigenere_Cipher;

public class VigenereKey : CipherKeyBase
{
    /// <summary>
    /// The memorable key.
    /// </summary>
    public string MemorableKey { get; set; }
}