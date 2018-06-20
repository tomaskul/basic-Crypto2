namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    /// <summary>
    /// Interface for Playfair cipher. Read more here: https://en.wikipedia.org/wiki/Playfair_cipher
    /// </summary>
    public interface IPlayfairCipher
    {        
        /// <summary>
        /// Generates the cipher key from specified plain text key to be used
        /// during encryption.
        /// </summary>
        /// <param name="key">Plain text key</param>
        /// <returns>2D array of characters that represents a cipher key.</returns>
        char[,] GenerateCipherKey(string key);

        /// <summary>
        /// Determines whether the specified cipher key is valid (i.e. matches all the
        /// necessary criteria).
        /// </summary>
        /// <param name="cipherKey">Cipher key to validate.</param>
        /// <returns><c>true</c> if a valid cipher key; otherwise <c>false</c>.</returns>
        bool IsValidCipherKey(char[,] cipherKey);
        
        /// <summary>
        /// Encrypts a plain text message by using the specified key.
        /// </summary>
        /// <param name="plainText">Text to encrypt.</param>
        /// <param name="key">Key to encrypt text with.</param>
        /// <returns>Encrypted plaintext</returns>
        string EncryptMessage(string plainText, string key);

        /// <summary>
        /// Encrypts a plain text message by using a cipher key.
        /// </summary>
        /// <param name="plainText">Text to encrypt.</param>
        /// <param name="cipherKey">2D playfair cipher key.</param>
        /// <returns></returns>
        string EncryptMessage(string plainText, char[,] cipherKey);
        
        /// <summary>
        /// Decrypts ciphertext message by using the specified key.
        /// </summary>
        /// <param name="cipherText">Plaintext encrypted via playfair key.</param>
        /// <param name="key">Key to use for decryption.</param>
        /// <returns>Plaintext derived by decrypting ciphertext via supplied key.</returns>
        string DecryptMessage(string cipherText, string key);

        /// <summary>
        /// Decypts a ciphertext message by using the specified cipher key.
        /// </summary>
        /// <param name="cipherText">Plaintext encrypted via playfair key.</param>
        /// <param name="cipherKey">2D playfair cipher key.</param>
        /// <returns></returns>
        string DecryptMessage(string cipherText, char[,] cipherKey);
    }
}