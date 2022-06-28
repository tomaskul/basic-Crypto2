using System;
using System.Linq;
using System.Text;

namespace SimpleCryptoLib.Ciphers.Caesar_Shift_Cipher
{
    /// <summary>Implementation of <see cref="ICaesarShiftCipher"/>.</summary>
    public class CaesarShiftCipher : ICaesarShiftCipher
    {
        public string EncryptMessage(string plainText, CaesarShiftCipherKey cipherKey)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                throw new ArgumentNullException(nameof(plainText));

            var sb = new StringBuilder(string.Empty);

            foreach (var character in plainText.ToUpper())
            {
                if (!cipherKey.Alphabet.Contains(character.ToString()))
                {
                    sb.Append(character);
                    continue;
                }

                var encryptedCharacterIndex = (cipherKey.Alphabet.IndexOf(character) + cipherKey.Shift) %
                                              cipherKey.Alphabet.Length;

                char encryptedCharacter;
                if (encryptedCharacterIndex >= 0)
                {
                    encryptedCharacter = cipherKey.Alphabet[encryptedCharacterIndex];
                }
                else
                {
                    var adjustedIndex = cipherKey.Alphabet.Length +
                                        (encryptedCharacterIndex % -cipherKey.Alphabet.Length);
                    encryptedCharacter = cipherKey.Alphabet.ElementAt(adjustedIndex);
                }

                sb.Append(encryptedCharacter);
            }

            return sb.ToString();
        }

        public string DecryptMessage(string cipherText, CaesarShiftCipherKey cipherKey)
        {
            cipherKey.Shift = -cipherKey.Shift;
            return EncryptMessage(cipherText, cipherKey).ToLower();
        }
    }
}