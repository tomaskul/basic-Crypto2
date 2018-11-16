using SimpleCryptoLib.Ciphers.Common.Key_Management;

namespace SimpleCryptoLib.Ciphers.Caesar_Shift_Cipher
{
    /// <inheritdoc />
    /// <summary>
    /// Caesar shift cipher key.
    /// </summary>
    public class CaesarShiftCipherKey : CipherKeyBase
    {
        /// <summary>
        /// Encryption / Decryption alphabet.
        /// </summary>
        public string Alphabet { get; set; }
        
        /// <summary>
        /// Encryption / Decryption shift.
        /// </summary>
        public int Shift { get; set; }
    }
}