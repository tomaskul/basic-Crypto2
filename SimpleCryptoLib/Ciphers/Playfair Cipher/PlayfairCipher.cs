using System;
using System.Collections.Generic;
using System.Text;
using SimpleCryptoLib.Ciphers.Playfair_Cipher.Digraphs;
using SimpleCryptoLib.Ciphers.Playfair_Cipher.Key_Management;

namespace SimpleCryptoLib.Ciphers.Playfair_Cipher;

public class PlayfairCipher : IPlayfairCipher
{
    private const int CipherGridDimension = 5; // Cipher specifies 5 by 5 grid for the key.

        private readonly IDigrathGenerator _digrathGenerator;
        private readonly IPlayfairKeyManagement _keyManagement;

        /// <summary>
        /// Constructs an object able to encrypt & decrypt messages using playfair cipher. Please note
        /// that this implementation only uses English alphabet and letter 'Q' is being omitted from the
        /// cipher key table. Numbers and special characters are ignored.
        /// </summary>
        /// <param name="digrathGenerator">Digrath generator.</param>
        /// <param name="playfairKeyManagement">Playfair key management.</param>
        public PlayfairCipher(IDigrathGenerator digrathGenerator, IPlayfairKeyManagement playfairKeyManagement)
        {
            _digrathGenerator = digrathGenerator;
            _keyManagement = playfairKeyManagement;
        }

        public string EncryptMessage(string plainText, string memorableKey)
        {
            var cipherKey = _keyManagement.GenerateCipherKey(memorableKey);
            var sanitizedMessage = PlayfairUtil.GetSanitisedString(plainText);
            var digraphs = _digrathGenerator.GetMessageDigraths(sanitizedMessage);

            return Encrypt(digraphs, cipherKey);
        }

        public string EncryptMessage(string plainText, PlayfairKey cipherKey)
        {
            if (!_keyManagement.IsValidCipherKey(cipherKey))
            {
                throw new ArgumentException("Invalid cipher key.");
            }

            var sanitizedMessage = PlayfairUtil.GetSanitisedString(plainText);
            var digraths = _digrathGenerator.GetMessageDigraths(sanitizedMessage);

            return Encrypt(digraths, cipherKey);
        }

        public string DecryptMessage(string cipherText, string memorableKey)
        {
            return Decrypt(_digrathGenerator.GetCipherTextDigraphs(cipherText),
                _keyManagement.GenerateCipherKey(memorableKey));
        }

        public string DecryptMessage(string cipherText, PlayfairKey cipherKey)
        {
            if (!_keyManagement.IsValidCipherKey(cipherKey))
            {
                throw new ArgumentException("Invalid cipher key.");
            }

            return Decrypt(_digrathGenerator.GetCipherTextDigraphs(cipherText), cipherKey);
        }

        /// <summary>
        /// Performs the encryption.
        /// </summary>
        /// <param name="digraths">Plain text digraphs.</param>
        /// <param name="key">Encryption cipher key.</param>
        /// <returns>Encrypted cipher text.</returns>
        private static string Encrypt(IEnumerable<Digraph> digraths, PlayfairKey key)
        {
            return PerformGenericPlayfair(digraths, key);
        }

        /// <summary>
        /// Performs decryption.
        /// </summary>
        /// <param name="digraphs">Cipher text digraphs.</param>
        /// <param name="key">Encryption cipher key.</param>
        /// <returns>Decrypted cipher text.</returns>
        private static string Decrypt(IEnumerable<Digraph> digraphs, PlayfairKey key)
        {
            return PerformGenericPlayfair(digraphs, key, true).ToLower();
        }
        
        #region Generic Algorithm Functions

        private static string PerformGenericPlayfair(IEnumerable<Digraph> digraphs, PlayfairKey key,
            bool isDecrypt = false)
        {
            var sb = new StringBuilder(string.Empty);

            foreach (var digraph in digraphs)
            {
                // Find digraph character index values.
                var charOneIndex = 0;
                var charTwoIndex = 0;
                for (var i = 0; i < key.Value.Length; i++)
                {
                    if (key.Value[i].Equals(digraph.CharacterOne))
                    {
                        charOneIndex = i;
                    }
                    else if (key.Value[i].Equals(digraph.CharacterTwo))
                    {
                        charTwoIndex = i;
                    }
                }

                if (AreOnTheSameRow(charOneIndex, charTwoIndex))
                {
                    sb.Append(GetCharacterSameRow(charOneIndex, key, isDecrypt));
                    sb.Append(GetCharacterSameRow(charTwoIndex, key, isDecrypt));
                }
                else if (AreInTheSameColumn(charOneIndex, charTwoIndex))
                {
                    sb.Append(GetCharacterSameColumn(charOneIndex, key, isDecrypt));
                    sb.Append(GetCharacterSameColumn(charTwoIndex, key, isDecrypt));
                }
                else
                {
                    sb.Append(GetSquareChar(charOneIndex, charTwoIndex, key, isDecrypt));
                    sb.Append(GetSquareChar(charOneIndex, charTwoIndex, key, isDecrypt, true));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Rule One: Characters are on the same row, replace by characters immediately to the right (encryption) / left
        /// (decryption) of each character in the digrath. If at the end of the grid (encryption), use initial element.
        /// If at the start of the grid (decryption), use last element.
        /// </summary>
        /// <param name="charOneIndex"></param>
        /// <param name="charTwoIndex"></param>
        /// <returns></returns>
        private static bool AreOnTheSameRow(int charOneIndex, int charTwoIndex)
        {
            return GetCharRowIndex(charOneIndex) == GetCharRowIndex(charTwoIndex);
        }

        /// <summary>
        /// Rule Two: Characters are in the same column, replace by characters immediately below (encryption) / above
        /// (decryption) each character in the digrath. If at the bottom (encryption), use top element. If at the top
        /// (decryption) use bottom element.
        /// </summary>
        /// <param name="charOneIndex"></param>
        /// <param name="charTwoIndex"></param>
        /// <returns></returns>
        private static bool AreInTheSameColumn(int charOneIndex, int charTwoIndex)
        {
            return GetCharColumnIndex(charOneIndex) == GetCharColumnIndex(charTwoIndex);
        }

        /// <summary>
        /// Based on the current index and whether attempting to encrypt/decrypt appropriate character is retrieved
        /// from the cipher key value. This method applies when both of the characters are on the same row.
        /// </summary>
        /// <param name="currentIndex">Current index of the character attempting to encrypt/decrypt.</param>
        /// <param name="key">Cipher key being used for encryption/decryption.</param>
        /// <param name="isDecryption">Flag determining whether to use decryption rule set. If not set to <c>true</c>,
        /// encryption mode is assumed.</param>
        /// <returns>Encrypted/Decrypted character corresponding to conditions specified.</returns>
        private static char GetCharacterSameRow(int currentIndex, PlayfairKey key, bool isDecryption = false)
        {
            // Decryption; Character immediately to the left.
            if (isDecryption)
            {
                // If left-most column: use end column character, otherwise use character to the left of current index.
                return GetCharColumnIndex(currentIndex) == 0
                    ? key.Value[currentIndex + (CipherGridDimension - 1)]
                    : key.Value[currentIndex - 1];
            }

            // Encryption; If character is right-most use first first character within the row, otherwise use
            // character immediately to the right.
            return GetCharColumnIndex(currentIndex + 1) == 0
                ? key.Value[currentIndex - (CipherGridDimension - 1)]
                : key.Value[currentIndex + 1];
        }

        /// <summary>
        /// Based on the current index and whether attempting to encrypt/decrypt appropriate character is retrieved
        /// from the cipher key value. This method applies when both of the characters are in the same column.
        /// </summary>
        /// <param name="currentIndex">Current index of the character attempting to encrypt/decrypt.</param>
        /// <param name="key">Cipher key being used for encryption/decryption.</param>
        /// <param name="isDecryption">Flag determining whether to use decryption rule set. If not set to <c>true</c>,
        /// encryption mode is assumed.</param>
        /// <returns>Encrypted/Decrypted character corresponding to conditions specified.</returns>
        private static char GetCharacterSameColumn(int currentIndex, PlayfairKey key, bool isDecryption = false)
        {
            // Decryption; Character immediately above.
            if (isDecryption)
            {
                // If top-most row: use bottom row, otherwise use character immediately above. The 4 multiplier makes
                // it easy to transition from top-most to bottom row within the key.
                return currentIndex < CipherGridDimension
                    ? key.Value[(CipherGridDimension * 4) + currentIndex]
                    : key.Value[currentIndex - CipherGridDimension];
            }

            // Encryption; If character is on the bottom-most row use character from the top row of the same column,
            // otherwise use character from the row below.
            return GetCharRowIndex(currentIndex) == (CipherGridDimension - 1)
                ? key.Value[currentIndex - (CipherGridDimension * 4)]
                : key.Value[currentIndex + CipherGridDimension];
        }

        private static char GetSquareChar(int charOneIndex, int charTwoIndex, PlayfairKey key,
            bool isDecryption = false, bool isSecondChar = false)
        {
            int row;

            if (isDecryption)
            {
                if (isSecondChar)
                {
                    row = GetCharRowIndex(charTwoIndex) * CipherGridDimension;
                    return key.Value[row + GetCharColumnIndex(charOneIndex)];
                }

                row = GetCharRowIndex(charOneIndex) * CipherGridDimension;
                return key.Value[row + GetCharColumnIndex(charTwoIndex)];
            }


            if (isSecondChar)
            {
                row = GetCharRowIndex(charTwoIndex) * CipherGridDimension;
                return key.Value[row + GetCharColumnIndex(charOneIndex)];
            }

            // Calculation inside Convert works out the row multiplier and multiplication provides the actual row index.
            row = GetCharRowIndex(charOneIndex) * CipherGridDimension;
            return key.Value[row + GetCharColumnIndex(charTwoIndex)];
        }

        /// <summary>
        /// Gets characters' row index by utilising its' absolute index value (in relation to cipher key).
        /// </summary>
        /// <param name="absoluteIndex">Character index within the cipher key.</param>
        /// <returns>Row index.</returns>
        private static int GetCharRowIndex(int absoluteIndex)
        {
            return Convert.ToInt32(Math.Floor((double) absoluteIndex / CipherGridDimension));
        }

        /// <summary>
        /// Gets characters' column index by utilising its' obsolete index value (in relation to cipher key).
        /// </summary>
        /// <param name="absoluteIndex">Character index within the cipher key.</param>
        /// <returns>Column index.</returns>
        private static int GetCharColumnIndex(int absoluteIndex)
        {
            return absoluteIndex % CipherGridDimension;
        }

        #endregion
}