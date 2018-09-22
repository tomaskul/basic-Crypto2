using System;
using System.Collections.Generic;
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

        public PlayfairKeyManagement()
        {
        }

        public PlayfairKey GenerateCipherKey(string memorableKey)
        {
            // Filter out all the unnecessary/redundant/unsupported characters.
            var sanitisedKey = GetSanitisedKey(PlayfairUtil.GetSanitisedString(memorableKey));

            return new PlayfairKey
            {
                // If necessary amount of characters is present then key is done, alternatively fill in missing values.
                Value = sanitisedKey.Length == Alphabet.Length
                    ? sanitisedKey
                    : GetCompleteCipherKey(sanitisedKey)
            };
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
                    && !character.Equals(OmittedCharacter) && Alphabet.Contains(character))
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
            var sb = new StringBuilder(incompleteKey);
            
            foreach (var letter in Alphabet)
            {
                if (!incompleteKey.Contains(letter))
                {
                    sb.Append(letter);
                }
            }

            return sb.ToString();
        }

        public bool IsValidCipherKey(PlayfairKey cipherKey)
        {
            if (cipherKey == null 
                || string.IsNullOrWhiteSpace(cipherKey.Value)
                || cipherKey.Value.Equals(Alphabet)
                || cipherKey.Value.Contains(OmittedCharacter)
                || cipherKey.Value.Length != Alphabet.Length
                || !cipherKey.Value.Equals(PlayfairUtil.GetSanitisedString(cipherKey.Value))
                || cipherKey.Value.Any(c => !char.IsLetter(c) || !char.IsUpper(c)))
            {
                return false;
            }

            // Once every other condition is met, need to ensure all characters are unique.
            var observedCharacters = new HashSet<char>();
            foreach (var character in cipherKey.Value)
            {
                if (observedCharacters.Contains(character))
                {
                    return false;
                }

                observedCharacters.Add(character);
            }

            return true;
        }
    }
}