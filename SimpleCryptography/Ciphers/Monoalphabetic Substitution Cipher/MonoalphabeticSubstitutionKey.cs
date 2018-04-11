using System.Collections.Generic;

namespace SimpleCryptography.Ciphers.Monoalphabetic_Substitution_Cipher
{
    public class MonoalphabeticSubstitutionKey
    {
        /// <summary>
        /// Key (plain text), Value (substitution) key object.
        /// </summary>
        public IDictionary<char, char> SubstitutionMapping { get; set; }

        public MonoalphabeticSubstitutionKey(IDictionary<char, char> mapping)
        {
            SubstitutionMapping = mapping;
        }
    }
}