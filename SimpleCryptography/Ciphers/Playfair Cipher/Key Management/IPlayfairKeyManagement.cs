﻿namespace SimpleCryptography.Ciphers.Playfair_Cipher.Key_Management
{
    /// <summary>
    /// Playfair key generation and validation.
    /// </summary>
    public interface IPlayfairKeyManagement
    {
        /// <summary>
        /// Generates a valid cipher key from specified memorable key.
        /// </summary>
        /// <param name="memorableKey">Memorable key that can be used to generate full cipher key</param>
        /// <returns>Playfair key used for encryption/decryption.</returns>
        PlayfairKey GenerateCipherKey(string memorableKey);
        
        /// <summary>
        /// Determines whether specified playfair cipher key is valid or not.
        /// </summary>
        /// <param name="cipherKey">Cipher key to validate.</param>
        /// <returns><c>true</c> if key is valid; otherwise <c>false</c>.</returns>
        bool IsValidCipherKey(PlayfairKey cipherKey);
    }
}