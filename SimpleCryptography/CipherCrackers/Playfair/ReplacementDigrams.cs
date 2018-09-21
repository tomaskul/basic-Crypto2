using System.Collections.Generic;
using SimpleCryptography.Ciphers.Playfair_Cipher;
using SimpleCryptography.Ciphers.Playfair_Cipher.Digrams;

namespace SimpleCryptography.CipherCrackers.Playfair
{
    /// <summary>
    /// Language and its' associated digrams to be analysed.
    /// </summary>
    public class ReplacementDigrams
    {
        /// <summary>
        /// Name of the language.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Most common digrams within the language.
        /// </summary>
        public IEnumerable<Digram> Digrams { get; set; }
    }
}