using SimpleCryptography.Ciphers.Generic.Key_Management;

namespace SimpleCryptography.Ciphers.Generic
{
    /// <summary>
    /// Generic cipher functions.
    /// </summary>
    /// <typeparam name="TCipherKey">Cipher key type</typeparam>
    public interface IGenericCipher<in TCipherKey> where TCipherKey : BaseCipherKey
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