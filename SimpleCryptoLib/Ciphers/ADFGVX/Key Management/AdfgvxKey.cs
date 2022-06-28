using SimpleCryptoLib.Ciphers.Common.Key_Management;

namespace SimpleCryptoLib.Ciphers.ADFGVX.Key_Management;

/// <summary>
/// ADFGVX Cipher Key.
/// </summary>
public class AdfgvxKey : CipherKeyBase
{
    /// <summary>
    /// ADFGVX Cipher key value.
    /// </summary>
    public string Value { get; set; }
}