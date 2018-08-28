using System;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public class PlayfairCipher : IPlayfairCipher
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPRSTUVWXYZ";
        private const char OmittedCharacter = 'Q';
        private const string AlphabetRegexPattern = @"[a-pr-zA-PR-Z]";
        private const int CipherGridDimension = 5; // Cipher specifies 5 by 5 grid for the key.

        private readonly IDigramGenerator _digramGenerator;

        /// <summary>
        /// Constructs an object able to encrypt & decrypt messages using playfair cipher. Please note
        /// that this implementaton only uses english alphabet and letter 'Q' is being omitted from the
        /// cipher key table. Numbers and special characters are ignored.
        /// </summary>
        public PlayfairCipher(IDigramGenerator digramGenerator)
        {
            _digramGenerator = digramGenerator;
        }

        /// <summary>
        /// Encrypts a plain text message by using the specified key.
        /// </summary>
        /// <param name="plainText">Text to encrypt.</param>
        /// <param name="key">Key to encrypt text with.</param>
        /// <returns>Encrypted plaintext</returns>
        public string EncryptMessage(string plainText, string key)
        {
            ThrowIfInvalidArgument(plainText, key);
            
            var cipherKey = GetCipherKey(key);
            var sanitizedMessage = GetSanitisedString(plainText);
            var digrams = _digramGenerator.GetMessageDigrams(sanitizedMessage);

            var sb = new StringBuilder(string.Empty);

            foreach (var digram in digrams)
            {
                var charOnePosition = new CharacterPosition(digram.CharacterOne);
                var charTwoPosition = new CharacterPosition(digram.CharacterTwo);

                for (var row = 0; row < CipherGridDimension; row++)
                {
                    for (var column = 0; column < CipherGridDimension; column++)
                    {
                        if (cipherKey[row, column].Equals(digram.CharacterOne))
                        {
                            charOnePosition.Row = row;
                            charOnePosition.Column = column;
                        }
                        else if (cipherKey[row, column].Equals(digram.CharacterTwo))
                        {
                            charTwoPosition.Row = row;
                            charTwoPosition.Column = column;
                        }
                    }
                }

                // Throw exception if for some reason necessary fields weren't initialised correctly.
                ThrowIfUninitialisedCharacterPosition(charOnePosition);
                ThrowIfUninitialisedCharacterPosition(charTwoPosition);

                if (charOnePosition.Row == charTwoPosition.Row)
                {
                    // Same row - replace by characters immediately to the right of each char in digram.
                    // if at the bottom of the grid, use top element.
                    
                    sb.Append(cipherKey[charOnePosition.Row.Value,
                        charOnePosition.Column.Value + 1 == CipherGridDimension
                            ? 0
                            : charOnePosition.Column.Value + 1]);

                    sb.Append(cipherKey[charTwoPosition.Row.Value,
                        charTwoPosition.Column.Value + 1 == CipherGridDimension
                            ? 0
                            : charTwoPosition.Column.Value + 1]);
                }
                else if (charOnePosition.Column == charTwoPosition.Column)
                {
                    // Same column - replace by characters immediately below each char in digram.
                    // If at the bottom of the grid, use top element.

                    sb.Append(cipherKey[charOnePosition.Row.Value + 1 == CipherGridDimension
                        ? 0
                        : charOnePosition.Row.Value + 1, charOnePosition.Column.Value]);

                    sb.Append(cipherKey[charTwoPosition.Row.Value + 1 == CipherGridDimension
                        ? 0
                        : charTwoPosition.Row.Value + 1, charTwoPosition.Column.Value]);
                }
                else
                {
                    // Neither same row nor same column - 1st char = char1 row & char2 column,
                    // 2nd char = char2 row & char1 column.
                    sb.Append(cipherKey[charOnePosition.Row.Value, charTwoPosition.Column.Value]);
                    sb.Append(cipherKey[charTwoPosition.Row.Value, charOnePosition.Column.Value]);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Encrypts a plain text message by using a cipher key.
        /// </summary>
        /// <param name="plainText">Text to encrypt.</param>
        /// <param name="cipherKey">2D playfair cypher key.</param>
        /// <returns></returns>
        public string EncryptMessage(string plainText, char[,] cipherKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Decrypts cipher text message by using the specified key.
        /// </summary>
        /// <param name="cipherText">Plaintext encrypted via playfair key.</param>
        /// <param name="key">Key to use for decryption.</param>
        /// <returns>Plaintext derived by decrypting ciphertext via supplied key.</returns>s
        public string DecryptMessage(string cipherText, string key)
        {
            ThrowIfInvalidArgument(cipherText, key);
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Decypts a cipher text message by using the specified cipher key.
        /// </summary>
        /// <param name="cipherText">Plaintext encrypted via playfair key.</param>
        /// <param name="cipherKey">2D playfair cipher key.</param>
        /// <returns></returns>
        public string DecryptMessage(string cipherText, char[,] cipherKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Throws an exception if any of the arguments used for encryption/decryption are considered invalid.
        /// </summary>
        /// <param name="message">Plain/Cipher text.</param>
        /// <param name="key">Key.</param>
        /// <exception cref="ArgumentNullException">Message/key is empty.</exception>
        private void ThrowIfInvalidArgument(string message, string key)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (!Regex.IsMatch(message, AlphabetRegexPattern))
            {
                throw new ArgumentException("Invalid message provided, must contain letters.");
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!Regex.IsMatch(key, AlphabetRegexPattern))
            {
                throw new ArgumentException("Invalid key provided, must contain letters");
            }
        }

        /// <summary>
        /// Applies the supported alphabets' regex to filter out unacceptable characters.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string GetSanitisedString(string input)
        {
            var regex = new Regex(AlphabetRegexPattern);
            var matches = regex.Matches(input.ToUpper(), 0);

            var sb = new StringBuilder(string.Empty);
            for (var i = 0; i < matches.Count; i++)
            {
                sb.Append(matches[i]);
            }

            var output = sb.ToString();
            if (string.IsNullOrEmpty(output))
            {
                throw new ArgumentException($"None of the characters within input '{input}' were valid.");
            }

            return output;
        }

        private void ThrowIfUninitialisedCharacterPosition(CharacterPosition characterPosition)
        {
            if (characterPosition.Row == null || characterPosition.Column == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(characterPosition)} X or Y positions weren't initialised.");
            }
        }

        #region Key generation

        /// <summary>
        /// Generates the cipher key from specified plain text key to be used
        /// during encryption.
        /// </summary>
        /// <param name="key">Plain text key</param>
        /// <returns>2D array of characters that represents a cipher key.</returns>
        public char[,] GenerateCipherKey(string key)
        {
            return GetCipherKey(key);
        }

        /// <summary>
        /// Determines whether the specified cipher key is valid (i.e. matches all the
        /// necessary criteria).
        /// </summary>
        /// <param name="cipherKey">Cipher key to validate.</param>
        /// <returns><c>true</c> if a valid cyipher key; otherwise <c>false</c>.</returns>
        /// <exception cref="NotImplementedException"></exception>
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
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the Playfair cipher key from specified key.
        /// </summary>
        /// <param name="inputKey">Key.</param>
        /// <returns>Playfair cipher key.</returns>
        private static char[,] GetCipherKey(string inputKey)
        {
            // 1. Remove garbage from the input.
            var sanitizedKey = GetSanitizedKey(GetSanitisedString(inputKey));

            // 2. Get complete key string.
            var keyString = sanitizedKey.Length == Alphabet.Length
                ? sanitizedKey
                : GetCompleteCipherKey(sanitizedKey);

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
        /// Gets the sanitized key for fundamental encryption/decryption part of the algorithm.
        /// </summary>
        /// <param name="key">Key used for encryption/decryption.</param>
        /// <returns>A string of unique characters representing the encryption key.</returns>
        private static string GetSanitizedKey(string key)
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

        #endregion
    }
}