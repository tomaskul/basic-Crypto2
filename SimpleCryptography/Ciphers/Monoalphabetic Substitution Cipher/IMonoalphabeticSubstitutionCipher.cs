namespace SimpleCryptography.Ciphers.Monoalphabetic_Substitution_Cipher
{
    public interface IMonoalphabeticSubstitutionCipher
    {
        /// <summary>
        /// Encrypts a plaintext message using the specified key.
        /// </summary>
        /// <param name="plainText">Text to be encrypted.</param>
        /// <param name="cipherKey">Key to use during encryption.</param>
        /// <returns>Encrypted plaintext.</returns>
        string EncryptMessage(string plainText, MonoalphabeticSubstitutionKey cipherKey);
        
        /// <summary>
        /// Decrypts ciphertext using the specified key.
        /// </summary>
        /// <param name="cipherText">Encrypted message to be decyphered.</param>
        /// <param name="cipherKey">Key to use during decryption.</param>
        /// <returns>Decrypted cyphertext.</returns>
        string DecryptMessage(string cipherText, MonoalphabeticSubstitutionKey cipherKey);
    }
}