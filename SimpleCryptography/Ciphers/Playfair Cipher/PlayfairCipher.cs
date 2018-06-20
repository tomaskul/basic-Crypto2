using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public class PlayfairCipher : IPlayfairCipher
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPRSTUVWXYZ"; // 'Q' omitted.
        private const string AlphabetRegexPattern = @"[a-pr-zA-PR-Z]";

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
            var digrams = _digramGenerator.GetMessageDigram(sanitizedMessage);

            var sb = new StringBuilder(string.Empty);

            foreach (var digram in digrams)
            {
                if (IsSameRow(digram, cipherKey))
                {
                    
                }
                else if (IsSameColumn(digram, cipherKey))
                {
                    
                }
                else
                {
                    
                }
            }
            
            
            #if DEBUG
            
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Console.Write($"{cipherKey[i, j]} ");
                }

                Console.WriteLine();
            }

            return "";

            #endif

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
        /// Decrypts cyphertext message by using the specified key.
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
        /// Decypts a cyphertext message by using the specified cypher key.
        /// </summary>
        /// <param name="cipherText">Plaintext encrypted via playfair key.</param>
        /// <param name="cipherKey">2D playfair cypher key.</param>
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

        private bool IsSameRow(Digram digram, char[,] cipherKey)
        {
            return false;
        }

        private bool IsSameColumn(Digram digram, char[,] cipherKey)
        {
            return false;
        }
        

        #region Key generation

        /// <summary>
        /// Generates the cypher key from specified plain text key to be used
        /// during encryption.
        /// </summary>
        /// <param name="key">Plain text key</param>
        /// <returns>2D array of characters that represents a cypher key.</returns>
        public char[,] GenerateCypherKey(string key)
        {
            return GetCipherKey(key);
        }
        
        /// <summary>
        /// Determines whether the specified cypher key is valid (i.e. matches all the
        /// necessary criteria).
        /// </summary>
        /// <param name="cypherKey">Cypher key to validate.</param>
        /// <returns><c>true</c> if a valid cypher key; otherwise <c>false</c>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsValidCypherKey(char[,] cypherKey)
        {
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

            // Cipher specifies 5 by 5 grid for the key. 
            const int gridDimension = 5;

            var cipherKey = new char[gridDimension, gridDimension];
            var counter = 0;
            for (var i = 0; i < gridDimension; i++)
            {
                for (var j = 0; j < gridDimension; j++)
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