using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SimpleCryptography.Ciphers.Playfair_Cipher.Key_Management;

namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public class PlayfairCipher : IPlayfairCipher
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPRSTUVWXYZ";
        private const char OmittedCharacter = 'Q';
        private const string AlphabetRegexPattern = @"[a-pr-zA-PR-Z]";
        private const int CipherGridDimension = 5; // Cipher specifies 5 by 5 grid for the key.

        private readonly IDigramGenerator _digramGenerator;
        private readonly IPlayfairKeyManagement _keyManagement;

        /// <summary>
        /// Constructs an object able to encrypt & decrypt messages using playfair cipher. Please note
        /// that this implementaton only uses english alphabet and letter 'Q' is being omitted from the
        /// cipher key table. Numbers and special characters are ignored.
        /// </summary>
        /// <param name="digramGenerator">Digram generator.</param>
        /// <param name="playfairKeyManagement">Playfair key management.</param>
        public PlayfairCipher(IDigramGenerator digramGenerator, IPlayfairKeyManagement playfairKeyManagement)
        {
            _digramGenerator = digramGenerator;
            _keyManagement = playfairKeyManagement;
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
            
            var cipherKey = _keyManagement.GenerateCipherKey(key);
            var sanitizedMessage = PlayfairUtil.GetSanitisedString(plainText);
            var digrams = _digramGenerator.GetMessageDigrams(sanitizedMessage);

            return Encrypt(digrams, cipherKey);
        }

        /// <summary>
        /// Encrypts a plain text message by using a cipher key.
        /// </summary>
        /// <param name="plainText">Text to encrypt.</param>
        /// <param name="cipherKey">2D playfair cypher key.</param>
        /// <returns></returns>
        public string EncryptMessage(string plainText, char[,] cipherKey)
        {
            if (!_keyManagement.IsValidCipherKey(cipherKey))
            {
                throw new ArgumentException("Invalid cipher key.");
            }
            
            
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
        
        private void ThrowIfUninitialisedCharacterPosition(CharacterPosition characterPosition)
        {
            if (characterPosition.Row == null || characterPosition.Column == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(characterPosition)} X or Y positions weren't initialised.");
            }
        }

        #region Encryption

        private string Encrypt(IEnumerable<Digram> digrams, char[,] cipherKey)
        {
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

        #endregion
    }
}