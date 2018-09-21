using System;
using System.Linq;
using System.Text;

namespace SimpleCryptography.Ciphers.Playfair_Cipher.Key_Management
{
    /// <summary>
    /// This implementation uses the English alphabet and character 'Q' is omitted.
    /// </summary>
    public class PlayfairKeyManagement : IPlayfairKeyManagement
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPRSTUVWXYZ";
        private const char OmittedCharacter = 'Q';
        private const int CipherGridDimension = 5;
        
        public PlayfairKeyManagement()
        {
        }
        
        public char[,] GenerateCipherKey(string key)
        {
            // 1. Remove garbage from the input.
            var sanitisedKey = GetSanitisedKey(PlayfairUtil.GetSanitisedString(key));
            
            // 2. Get complete key string.
            var keyString = sanitisedKey.Length == Alphabet.Length
                ? sanitisedKey
                : GetCompleteCipherKey(sanitisedKey);

            var cipherKey = new char[CipherGridDimension, CipherGridDimension];
            var counter = 0;
            for (var i = 0; i < CipherGridDimension; i++)
            {
                for (var j = 0; j < CipherGridDimension; j++)
                {
                    cipherKey[i, j] = keyString[counter];
                    counter++;
                }
            }

            return cipherKey;
        }
        
        /// <summary>
        /// Gets the sanitised key for fundamental encryption/decryption part of the algorithm.
        /// </summary>
        /// <param name="key">Key used for encryption/decryption.</param>
        /// <returns>A string of unique characters representing the encryption key.</returns>
        private static string GetSanitisedKey(string key)
        {
            var sb = new StringBuilder(string.Empty);

            foreach (var character in key)
            {
                // Only record; Unique Alphabet characters.
                if (!sb.ToString().Any(c => c.Equals(character))
                    && !character.Equals('Q') && Alphabet.Contains(character))
                {
                    sb.Append(character);
                }
            }

            var sanitizedKey = sb.ToString();
            if (Alphabet.Equals(sanitizedKey))
            {
                // Sanitized key that's identical to the alphabet essentially provides negative security (i.e. could
                // lead to false sense of security when there is none). This does slightly reduce the search space,  
                // however, this algorithm was never that secure anyway, so search space isn't the biggest concern.
                throw new InvalidOperationException("Key cannot be identical to the cipher alphabet.");
            }

            return sanitizedKey;
        }
        
        /// <summary>
        /// Adds missing letters to the sanitized key in order to complete the cipher key.
        /// </summary>
        /// <param name="incompleteKey">Incomplete, sanitized key.</param>
        /// <returns>Playfair cipher key.</returns>
        private static string GetCompleteCipherKey(string incompleteKey)
        {
            foreach (var letter in Alphabet)
            {
                if (!incompleteKey.Contains(letter))
                {
                    incompleteKey += letter;
                }
            }

            return incompleteKey;
        }
        
        public bool IsValidCipherKey(char[,] cipherKey)
        {
            // Ensure that the dimensions match the requirements.
            if (cipherKey.Rank != 2 || cipherKey.GetLength(0) != CipherGridDimension ||
                cipherKey.GetLength(1) != CipherGridDimension)
            {
                return false;
            }

            var cipherKeyString = string.Empty;
            for (var row = 0; row < CipherGridDimension; row++)
            {
                for (var column = 0; column < CipherGridDimension; column++)
                {
                    var currentCharacter = cipherKey[row, column]; 
                    
                    // Only letters are allowed.
                    if (!char.IsLetter(currentCharacter) || !Alphabet.Contains(currentCharacter)) { return false; }

                    // Only unique, non-omitted letters allowed.
                    if (cipherKeyString.Contains(currentCharacter) || OmittedCharacter.Equals(currentCharacter))
                    {
                        return false;
                    }

                    cipherKeyString += currentCharacter;

                }
            }

            // Cipher key generation prevents the key from being same as the alphabet.
            return !cipherKeyString.Equals(Alphabet);
        }
    }
}