using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public static class PlayfairUtil
    {
        private const string AlphabetRegexPattern = @"[a-pr-zA-PR-Z]";
        
        /// <summary>
        /// Applies the supported alphabets' regex to filter out unacceptable characters.
        /// </summary>
        /// <param name="input">Input to sanitise.</param>
        /// <returns>String comprised of all upper case, accepted characters.</returns>
        /// <exception cref="ArgumentException">If none of the the characters within input were valid.</exception>
        public static string GetSanitisedString(string input)
        {
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
    }
}