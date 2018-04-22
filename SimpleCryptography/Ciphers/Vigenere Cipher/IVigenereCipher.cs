namespace SimpleCryptography.Ciphers.Vigenere_Cipher
{
    public interface IVigenereCipher
    {
        /// <summary>
        /// Encrypts a plain text message using specified alphabet and key.
        /// </summary>
        /// <param name="plainText">Message to encrypt.</param>
        /// <param name="alphabet">Encryption alphabet.</param>
        /// <param name="key">Encryption key.</param>
        /// <returns></returns>
        string EncryptMessage(string plainText, string alphabet, string key);

        /// <summary>
        /// Decrypts a cipher text message using specified alphabet and key.
        /// </summary>
        /// <param name="cipherText">Message to decrypt.</param>
        /// <param name="alphabet">Encryption alphabet.</param>
        /// <param name="key">Encryption key.</param>
        /// <returns></returns>
        string DecryptMessage(string cipherText, string alphabet, string key);
    }
}