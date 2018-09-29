using SimpleCryptography.Ciphers.Generic;

namespace SimpleCryptography.Ciphers.Vigenere_Cipher
{
    /// <summary>
    /// Vigenere cipher.
    /// </summary>
    public interface IVigenereCipher : IGenericCipher<VigenereKey>
    {
    }
}