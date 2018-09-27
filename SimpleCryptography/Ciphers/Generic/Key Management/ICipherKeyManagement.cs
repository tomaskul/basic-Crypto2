namespace SimpleCryptography.Ciphers.Generic.Key_Management
{
    /// <summary>
    /// Generic cipher key generation and validation.
    /// </summary>
    /// <typeparam name="TCipherKey">Cipher key type.</typeparam>
    public interface ICipherKeyManagement<TCipherKey> where TCipherKey : BaseCipherKey
    {
        /// <summary>
        /// Generates a valid cipher key from specified memorable key.
        /// </summary>
        /// <param name="memorableKey">Memorable key that can be used to generate full cipher key</param>
        /// <returns>Cipher key to be used for encryption/decryption.</returns>
        TCipherKey GenerateCipherKey(string memorableKey);

        /// <summary>
        /// Determines whether specified cipher key is valid or not.
        /// </summary>
        /// <param name="cipherKey">Cipher key to evaluate.</param>
        /// <returns><c>true</c> if key is valid; otherwise <c>false</c>.</returns>
        bool IsValidCipherKey(TCipherKey cipherKey);
    }
}