using SimpleCryptography.Ciphers.Generic.Key_Management;

namespace SimpleCryptography.Ciphers.Playfair_Cipher.Key_Management
{   
    /// <summary>
    /// Playfair cipher key.
    /// </summary>
    public class PlayfairKey : BaseCipherKey
    {
        /// <summary>
        /// Playfair cipher key value.
        /// </summary>
        public string Value { get; set; }
    }
}