namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public interface IPlayfairCipher
    {
        /// <summary>
        /// Encrypts a plaintext message by using the specified key.
        /// </summary>
        /// <param name="plainText">Text to encrypt.</param>
        /// <param name="key">Key to encrypt text with.</param>
        /// <returns>Encrypted plaintext</returns>
        string EncryptMessage(string plainText, string key);

        /// <summary>
        /// Decrypts cyphertext message by using the specified key.
        /// </summary>
        /// <param name="cipherText">Plaintext encrypted by Playfair encryption.</param>
        /// <param name="key">Key to use for decryption.</param>
        /// <returns>Plaintext derived by decrypting ciphertext via supplied key.</returns>
        string DecryptMessage(string cipherText, string key);
    }
}