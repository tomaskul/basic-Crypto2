using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCryptography.Ciphers.Caesar_Shift_Cipher;

namespace SimpleCryptography.Ciphers.Vigenere_Cipher
{
    public class VigenereCipher : IVigenereCipher
    {
        private static readonly CaesarShiftCipher CaesarShiftCipher = new CaesarShiftCipher();

        public VigenereCipher()
        {
        }

        public string EncryptMessage(string plainText, string alphabet, string key)
        {
            ThrowIfParametersAreInvalid(plainText, alphabet, key);

            var sb = new StringBuilder(string.Empty);
            var encryptionAlphabets = GetEncryptionAlphabets(alphabet, key);

            alphabet = alphabet.ToUpper();
            key = key.ToUpper();
            var keyIndex = 0;

            foreach (var character in plainText.ToUpper())
            {
                if (!alphabet.Contains(character))
                {
                    sb.Append(character);
                    continue;
                }

                var multiAlphabetIndex = keyIndex % key.Length;
                var currentAlphabet = encryptionAlphabets.First(a => a.First().Equals(key[multiAlphabetIndex]));

                sb.Append(currentAlphabet[alphabet.IndexOf(character)]);
                    
                // Possibly excesive, but prevents keyIndex from overflowing when encrypting huge messages.
                keyIndex = keyIndex == (key.Length - 1) ? 0 : keyIndex + 1;
            }

            return sb.ToString();
        }

        public string DecryptMessage(string cipherText, string alphabet, string key)
        {
            ThrowIfParametersAreInvalid(cipherText, alphabet, key);

            var sb = new StringBuilder(string.Empty);
            var encryptionAlphabets = GetEncryptionAlphabets(alphabet, key);

            var keyIndex = 0;

            foreach (var character in cipherText.ToUpper())
            {
                if (!alphabet.Contains(character))
                {
                    sb.Append(character);
                    continue;
                }
                
                var multiAlphabetIndex = keyIndex % key.Length;
                var currentAlphabet = encryptionAlphabets.First(a => a.First().Equals(key[multiAlphabetIndex]));

                sb.Append(alphabet[currentAlphabet.IndexOf(character)]);

                // Possibly excesive, but prevents keyIndex from overflowing when encrypting huge messages.
                keyIndex = keyIndex == (key.Length - 1) ? 0 : keyIndex + 1;
            }

            return sb.ToString().ToLower();
        }

        /// <summary>
        /// Pre encryption/decryption parameter validity check. If any of the preconditions required for
        /// encryption/decryption to execute successfully aren't met, this method will throw an exception.
        /// </summary>
        /// <param name="message">Message to encrypt/decrypt.</param>
        /// <param name="alphabet">Alphabet used during encryption/decryption.</param>
        /// <param name="key">Encryption/Decryption key.</param>
        /// <exception cref="ArgumentNullException"></exception>
        private void ThrowIfParametersAreInvalid(string message, string alphabet, string key)
        {
            if (string.IsNullOrWhiteSpace(message)) { throw new ArgumentNullException(nameof(message)); }
            if (string.IsNullOrWhiteSpace(alphabet)) { throw new ArgumentNullException(nameof(alphabet)); }
            if (string.IsNullOrWhiteSpace(key)) { throw new ArgumentNullException(nameof(key)); }
        }

        /// <summary>
        /// Gets all the alphabets involved in the encryption for a specified key.
        /// </summary>
        /// <param name="alphabet">The alphabet used for encryption.</param>
        /// <param name="key">Encryption key.</param>
        /// <returns></returns>
        private static List<string> GetEncryptionAlphabets(string alphabet, string key)
        {
            return alphabet
                .Select((t, i) => CaesarShiftCipher.EncryptMessage(alphabet, alphabet, i))
                .Where(offset => key.Any(c => c.Equals(offset.First())))
                .ToList();
        }
    }
}