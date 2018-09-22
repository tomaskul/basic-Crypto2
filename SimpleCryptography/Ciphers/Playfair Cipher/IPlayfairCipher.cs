﻿using SimpleCryptography.Ciphers.Playfair_Cipher.Key_Management;

namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    /// <summary>
    /// Interface for Playfair cipher. Read more here: https://en.wikipedia.org/wiki/Playfair_cipher
    /// </summary>
    public interface IPlayfairCipher
    {
        /// <summary>
        /// Encrypts a plain text message by using the specified key.
        /// </summary>
        /// <param name="plainText">Text to encrypt.</param>
        /// <param name="memorableKey">Memorable key to encrypt text with.</param>
        /// <returns>Encrypted plaintext</returns>
        string EncryptMessage(string plainText, string memorableKey);

        /// <summary>
        /// Encrypts a plain text message by using a cipher key.
        /// </summary>
        /// <param name="plainText">Text to encrypt.</param>
        /// <param name="cipherKey">Playfair cipher key.</param>
        /// <returns>Encrypted plaintext</returns>
        string EncryptMessage(string plainText, PlayfairKey cipherKey);
        
        /// <summary>
        /// Decrypts ciphertext message by using the specified key.
        /// </summary>
        /// <param name="cipherText">Plaintext encrypted via playfair key.</param>
        /// <param name="memorableKey">Key to use for decryption.</param>
        /// <returns>Plaintext derived by decrypting ciphertext via supplied key.</returns>
        string DecryptMessage(string cipherText, string memorableKey);

        /// <summary>
        /// Decypts a ciphertext message by using the specified cipher key.
        /// </summary>
        /// <param name="cipherText">Plaintext encrypted via playfair key.</param>
        /// <param name="cipherKey">Playfair cipher key.</param>
        /// <returns>Plaintext derived by decrypting ciphertext via supplied key.</returns>
        string DecryptMessage(string cipherText, PlayfairKey cipherKey);
    }
}