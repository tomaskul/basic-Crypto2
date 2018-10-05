using SimpleCryptography.Ciphers.Generic.Key_Management;

namespace SimpleCryptography.Ciphers.Vigenere_Cipher
{
    public class VigenereKey : BaseCipherKey
    {
        /// <summary>
        /// The memorable key.
        /// </summary>
        public string MemorableKey { get; set; }
    }
}