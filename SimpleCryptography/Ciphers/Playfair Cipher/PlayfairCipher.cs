using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public class PlayfairCipher : IPlayfairCipher
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPRSTUVWXYZ"; // 'Q' omitted.
        
        /// <summary>
        /// Constructs an object able to encrypt & decrypt messages using playfair cipher. Please note
        /// that this implementaton only uses english alphabet and letter 'Q' is being omitted from the
        /// cipher key table. Numbers and special characters are ignored.
        /// </summary>
        public PlayfairCipher()
        {
        }
        
        public string EncryptMessage(string plainText, string key)
        {
            ThrowIfInvalidArgument(plainText, key);
            var cipherKey = GetCipherKey(key);
            
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Console.Write($"{cipherKey[i, j]} ");
                }
                Console.WriteLine();
            }
            return "";
            
            throw new System.NotImplementedException();
        }

        public string DecryptMessage(string cipherText, string key)
        {
            ThrowIfInvalidArgument(cipherText, key);
            throw new System.NotImplementedException();
        }
        
        /// <summary>
        /// Throws an exception if any of the arguments used for encryption/decryption are considered invalid.
        /// </summary>
        /// <param name="message">Plain/Cipher text.</param>
        /// <param name="key">Key.</param>
        /// <exception cref="ArgumentNullException">Message/key is empty.</exception>
        private void ThrowIfInvalidArgument(string message, string key)
        {
            const string alphabetPattern = @"[a-pr-zA-PR-Z]";
            
            if (string.IsNullOrWhiteSpace(message)){ throw new ArgumentNullException(nameof(message)); }
            if (!Regex.IsMatch(message, alphabetPattern))
            {
                throw new ArgumentException("Invalid message provided, must contain letters.");
            }
            
            if (string.IsNullOrWhiteSpace(key)){ throw new ArgumentNullException(nameof(key)); }
            if (!Regex.IsMatch(key, alphabetPattern))
            {
                throw new ArgumentException("Invalid key provided, must contain letters");
            }
        }

        /// <summary>
        /// Gets the Playfair cipher key from specified key.
        /// </summary>
        /// <param name="inputKey">Key.</param>
        /// <returns>Playfair cipher key.</returns>
        private static char[,] GetCipherKey(string inputKey)
        {
            // 1. Remove garbage from the input.
            var sanitizedKey = GetSanitizedKey(inputKey);

            // 2. Get complete key string.
            var keyString = sanitizedKey.Length == Alphabet.Length
                ? sanitizedKey
                : GetCompleteCipherKey(sanitizedKey);
            
            var cipherKey = new char[5,5];
            var counter = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
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
        /// <returns>A string of unique, upper case letters, excluding 'Q'.</returns>
        private static string GetSanitizedKey(string key)
        {
            var sanitizedKey = string.Empty;

            foreach (var character in key.ToUpper())
            {
                if (!char.IsLetter(character)){ continue; }
                
                // Only record; Unique Enlish letters, except for 'Q'.
                if (!sanitizedKey.Any(c => c.Equals(character)) && !character.Equals('Q') && Alphabet.Contains(character))
                {
                    sanitizedKey += character;
                }
            }
            
            if (string.IsNullOrEmpty(sanitizedKey) || Alphabet.Equals(sanitizedKey))
            {
                // Empty sanitized key would lead to algorithm generating cipher key that's exact copy
                // of the alphabet which is a no-no, same goes for specified key that's already the alphabet.
                throw new InvalidOperationException("Invalid key specified.");
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

        
        private static string GetMessageDigram(string plainText)
        {
            // 1. Remove garbage from the input.
            var sanitizedMessage = GetSanitizedMessage(plainText);

            // 1a. If
            if (sanitizedMessage.Length == 0) { return sanitizedMessage; }
            
            for (var i = 0; i < sanitizedMessage.Length; i++)
            {
                
            }
            
            return "";
        }

        /// <summary>
        /// Removes all non alphabetical characters from the message string and outputs an all upper case string.
        /// </summary>
        /// <param name="inputMessage">Message to sanitize.</param>
        /// <returns>Sanitized message which doesn't contain any non-alphabetic characters.</returns>
        private static string GetSanitizedMessage(string inputMessage)
        {
            var sb = new StringBuilder(string.Empty);
            
            foreach (var character in inputMessage.ToUpper())
            {
                if (!char.IsLetter(character)) { continue; }

                sb.Append(character);
            }

            return sb.ToString();
        }
    }
}