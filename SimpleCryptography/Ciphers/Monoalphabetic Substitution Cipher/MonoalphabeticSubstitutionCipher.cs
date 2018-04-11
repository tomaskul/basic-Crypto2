using System;
using System.Linq;
using System.Text;

namespace SimpleCryptography.Ciphers.Monoalphabetic_Substitution_Cipher
{
    public class MonoalphabeticSubstitutionCipher : IMonoalphabeticSubstitutionCipher
    {

        public MonoalphabeticSubstitutionCipher()
        {
        }
        
        
        public string EncryptMessage(string plainText, MonoalphabeticSubstitutionKey cipherKey)
        {
            if (string.IsNullOrWhiteSpace(plainText)){ throw new ArgumentNullException(nameof(plainText)); }
            if (cipherKey == null || !cipherKey.SubstitutionMapping.Any()) { throw new ArgumentNullException(nameof(cipherKey)); }
            
            var sb = new StringBuilder(string.Empty);
            
            foreach (var c in plainText.ToLower())
            {
                if (cipherKey.SubstitutionMapping.Any(mapping => mapping.Key.Equals(c)))
                {
                    sb.Append(cipherKey.SubstitutionMapping
                        .First(mapping => mapping.Key.Equals(c))
                        .Value);
                }
            }
            
            return sb.ToString().ToUpper();
        }

        public string DecryptMessage(string cipherText, MonoalphabeticSubstitutionKey cipherKey)
        {
            if (string.IsNullOrWhiteSpace(cipherText)){ throw new ArgumentNullException(nameof(cipherText)); }
            if (cipherKey == null || !cipherKey.SubstitutionMapping.Any()) { throw new ArgumentNullException(nameof(cipherKey)); }
            
            var sb = new StringBuilder(string.Empty);

            foreach (var c in cipherText.ToUpper())
            {
                if (cipherKey.SubstitutionMapping.Any(mapping => mapping.Value.Equals(c)))
                {
                    sb.Append(cipherKey.SubstitutionMapping
                        .First(mapping => mapping.Value.Equals(c))
                        .Key);
                }
            }
            
            return sb.ToString().ToLower();
        }
    }
}