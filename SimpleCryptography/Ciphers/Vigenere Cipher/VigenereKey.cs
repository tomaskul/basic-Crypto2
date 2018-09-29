using System.ComponentModel.DataAnnotations;
using SimpleCryptography.Ciphers.Generic.Key_Management;

namespace SimpleCryptography.Ciphers.Vigenere_Cipher
{
    public class VigenereKey : BaseCipherKey
    {
        /// <summary>
        /// The memorable key.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Invalid memorable key")]
        public string MemorableKey { get; set; }
    }
}