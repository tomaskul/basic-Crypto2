using System;
using System.Collections.Generic;
using System.Text;
using SimpleCryptography.Ciphers.Playfair_Cipher.Digraths;
using SimpleCryptography.Ciphers.Playfair_Cipher.Key_Management;

namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public class PlayfairCipher : IPlayfairCipher
    {
        private const int CipherGridDimension = 5; // Cipher specifies 5 by 5 grid for the key.

        private readonly IDigrathGenerator _digrathGenerator;
        private readonly IPlayfairKeyManagement _keyManagement;

        /// <summary>
        /// Constructs an object able to encrypt & decrypt messages using playfair cipher. Please note
        /// that this implementaton only uses English alphabet and letter 'Q' is being omitted from the
        /// cipher key table. Numbers and special characters are ignored.
        /// </summary>
        /// <param name="digrathGenerator">Digrath generator.</param>
        /// <param name="playfairKeyManagement">Playfair key management.</param>
        public PlayfairCipher(IDigrathGenerator digrathGenerator, IPlayfairKeyManagement playfairKeyManagement)
        {
            _digrathGenerator = digrathGenerator;
            _keyManagement = playfairKeyManagement;
        }
        
        public string EncryptMessage(string plainText, string key)
        {
            var cipherKey = _keyManagement.GenerateCipherKey(key);
            var sanitizedMessage = PlayfairUtil.GetSanitisedString(plainText);
            var digraphs = _digrathGenerator.GetMessageDigraths(sanitizedMessage);

            return Encrypt(digraphs, cipherKey);
        }

        public string EncryptMessage(string plainText, char[,] cipherKey)
        {
            if (!_keyManagement.IsValidCipherKey(cipherKey)) { throw new ArgumentException("Invalid cipher key."); }
            var sanitizedMessage = PlayfairUtil.GetSanitisedString(plainText);
            var digraths = _digrathGenerator.GetMessageDigraths(sanitizedMessage);

            return Encrypt(digraths, cipherKey);
        }

        public string DecryptMessage(string cipherText, string key)
        {
            throw new System.NotImplementedException();
        }
        
        public string DecryptMessage(string cipherText, char[,] cipherKey)
        {
            throw new NotImplementedException();
        }

        #region Encryption

        private static string Encrypt(IEnumerable<Digraph> digraths, char[,] cipherKey)
        {
            var sb = new StringBuilder(string.Empty);

            foreach (var digraph in digraths)
            {
                var charOnePosition = new CharacterPosition(digraph.CharacterOne);
                var charTwoPosition = new CharacterPosition(digraph.CharacterTwo);

                for (var row = 0; row < CipherGridDimension; row++)
                {
                    for (var column = 0; column < CipherGridDimension; column++)
                    {
                        if (cipherKey[row, column].Equals(digraph.CharacterOne))
                        {
                            charOnePosition.Row = row;
                            charOnePosition.Column = column;
                        }
                        else if (cipherKey[row, column].Equals(digraph.CharacterTwo))
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
                    // if at the end of the grid, use initial element.
                    
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
        
        private static void ThrowIfUninitialisedCharacterPosition(CharacterPosition characterPosition)
        {
            if (characterPosition.Row == null || characterPosition.Column == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(characterPosition)} X or Y positions weren't initialised.");
            }
        }

        #endregion
    }
}