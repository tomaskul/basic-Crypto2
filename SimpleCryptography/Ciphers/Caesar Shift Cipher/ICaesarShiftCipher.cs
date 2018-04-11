namespace SimpleCryptography.Ciphers.Caesar_Shift_Cipher
{
    public interface ICaesarShiftCipher
    {
        /// <summary>
        /// Encrypts a plaintext message by using specified shift offset using specified alphabet.
        /// </summary>
        /// <param name="plainText">Text to encrypt.</param>
        /// <param name="alphabet">Alphabet used by the text.</param>
        /// <param name="shift">Shift on the alphabet.</param>
        /// <returns>Encrypted plaintext</returns>
        string EncryptMessage(string plainText, string alphabet, int shift);
        
        /// <summary>
        /// Decrypts ciphertext message using specified alphabet and shift offset.
        /// </summary>
        /// <param name="cipherText">Ciphertext to decrypt.</param>
        /// <param name="alphabet">Alpahbet used by the message.</param>
        /// <param name="shift">Shift on the alphabet.</param>
        /// <returns></returns>
        string DecryptMessage(string cipherText, string alphabet, int shift);
    }
}