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

        public string EncryptMessage(string plainText, string alphabet, int shift)
        {
            if (string.IsNullOrWhiteSpace(plainText)) { throw new ArgumentNullException(nameof(plainText)); }

            var sb = new StringBuilder(string.Empty);

            foreach (var character in plainText.ToUpper())
            {
                if (!alphabet.Contains(character.ToString()))
                {
                    sb.Append(character);
                    continue;
                }

                var encryptedCharacterIndex = (alphabet.IndexOf(character) + shift) % alphabet.Length;
                
                char encryptedCharacter;

                if (encryptedCharacterIndex >= 0)
                {
                    encryptedCharacter = alphabet[encryptedCharacterIndex];
                }
                else
                {
                    var adjustedIndex = alphabet.Length + (encryptedCharacterIndex % - alphabet.Length);
                    encryptedCharacter = alphabet.ElementAt(adjustedIndex);
                }

                sb.Append(encryptedCharacter);
            }

            return sb.ToString();
        }

        public string DecryptMessage(string cipherText, string alphabet, int shift)
        {
            return EncryptMessage(cipherText, alphabet, -shift).ToLower();
        }
    }
}