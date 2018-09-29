using System.ComponentModel.DataAnnotations;
using SimpleCryptography.Ciphers.Generic.Key_Management;

namespace SimpleCryptography.Ciphers.Caesar_Shift_Cipher
{
    /// <summary>
    /// Caesar cipher key.
    /// </summary>
    public class CaesarCipherKey : BaseCipherKey
    {
        /// <summary>
        /// Encryption/Decryption alphabet.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Alphabet { get; set; }
        
        /// <summary>
        /// Encryption/Decryption shift.
        /// </summary>
        [Required]
        public int Shift { get; set; }
    }
}