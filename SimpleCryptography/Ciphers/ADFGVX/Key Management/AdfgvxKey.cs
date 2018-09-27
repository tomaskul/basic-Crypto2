using SimpleCryptography.Ciphers.Generic.Key_Management;

namespace SimpleCryptography.Ciphers.ADFGVX.Key_Management
{
    /// <summary>
    /// ADFGVX Cipher Key.
    /// </summary>
    public class AdfgvxKey : BaseCipherKey
    {
        /// <summary>
        /// ADFGVX Cipher key value.
        /// </summary>
        public string Value { get; set; }
    }
}