using SimpleCryptoLib.Ciphers.Common.Key_Management;

namespace SimpleCryptoLib.Ciphers.Playfair_Cipher.Key_Management;

/// <summary>
/// Playfair cipher key.
/// </summary>
public class PlayfairKey : CipherKeyBase
{
    /// <summary>
    /// Playfair cipher key value.
    /// </summary>
    public string Value { get; set; }
}