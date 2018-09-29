using System;
using System.Linq;
using System.Text;

namespace SimpleCryptography.Ciphers.Caesar_Shift_Cipher
{
    public class CaesarShiftCipher : ICaesarShiftCipher
    {
        public CaesarShiftCipher()
        {
        }
        
        public string EncryptMessage(string plainText, CaesarCipherKey cipherKey)
        {
            if (string.IsNullOrWhiteSpace(plainText)) { throw new ArgumentNullException(nameof(plainText)); }

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

        public string DecryptMessage(string cipherText, CaesarCipherKey cipherKey)
        {
            cipherKey.Shift = -cipherKey.Shift;
            return EncryptMessage(cipherText, cipherKey).ToLower();
        }
    }
}