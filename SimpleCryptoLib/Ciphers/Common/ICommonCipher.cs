using SimpleCryptoLib.Ciphers.Common.Key_Management;

namespace SimpleCryptoLib.Ciphers.Common
{
    /// <summary>
    /// Common cipher functions.
    /// </summary>
    /// <typeparam name="TCipherKey">Cipher key type</typeparam>
    public interface ICommonCipher<in TCipherKey> where TCipherKey : CipherKeyBase
    {
        /// <summary>
        /// Encrypt plain text using specified cipher key.
        /// </summary>
        /// <param name="plainText">Plain text to encrypt.</param>
        /// <param name="cipherKey">Cipher key to use for encryption.</param>
        /// <returns>Encrypted plain text.</returns>
        string EncryptMessage(string plainText, TCipherKey cipherKey);
        
        /// <summary>
        /// Decrypt cipherText using specified cipher key.
        /// </summary>
        /// <param name="cipherText">Cipher text to decrypt.</param>
        /// <param name="cipherKey">Cipher key to use for decryption.</param>
        /// <returns>Decrypted cipher text.</returns>
        string DecryptMessage(string cipherText, TCipherKey cipherKey);
    }
}