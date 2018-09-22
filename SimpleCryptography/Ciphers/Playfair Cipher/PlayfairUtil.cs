using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public static class PlayfairUtil
    {
        /// <summary>
        /// Digraphs are composed in multiples of 2, which means the value bears significant importance when dealing
        /// with this cipher.
        /// </summary>
        public static int DigrathDenominator => 2;

        private const string AlphabetRegexPattern = @"[a-pr-zA-PR-Z]";
        private const char OmittedCharacter = 'Q';
        
        /// <summary>
        /// Applies the supported alphabets' regex to filter out unacceptable characters.
        /// </summary>
        /// <param name="input">Input to sanitise.</param>
        /// <returns>String comprised of all upper case, accepted characters.</returns>
        /// <exception cref="ArgumentException">If none of the the characters within input were valid.</exception>
        public static string GetSanitisedString(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) { throw new ArgumentNullException(nameof(input)); }
            
            var regex = new Regex(AlphabetRegexPattern);
            var matches = regex.Matches(input.ToUpper(), 0);

            if (matches.Count == 0)
            {
                throw new ArgumentException($"None of the characters within input '{input}' were valid.");
            }

            var sb = new StringBuilder(string.Empty);
            for (var i = 0; i < matches.Count; i++)
            {
                sb.Append(matches[i]);
            }

            return sb.ToString();
        }

        public static bool IsValidCipherText(string cipherText)
        {
            // Not permitted: empty strings, non-alphabetic characters (i.e. special characters, numbers),
            // omited character, non-upper case letters.
            if (string.IsNullOrWhiteSpace(cipherText) 
                || cipherText.Any(c => !char.IsLetter(c) || !char.IsUpper(c) || c == OmittedCharacter))
            {
                return false;
            }
            // Since the cipher text is constructed from digrams, the length is always an even number.
            if (cipherText.Length % DigrathDenominator != 0) { return false; }
            
            // If none of the conditions are broken then the cipher text is valid.
            return true;
        }
    }
}