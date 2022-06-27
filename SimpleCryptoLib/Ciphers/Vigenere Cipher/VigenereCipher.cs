using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SimpleCryptoLib.Ciphers.Caesar_Shift_Cipher;

namespace SimpleCryptoLib.Ciphers.Vigenere_Cipher;

public class VigenereCipher : IVigenereCipher
{
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string RegexAlphabetPattern = "[a-zA-Z]";
    private static readonly CaesarShiftCipher CaesarShiftCipher = new CaesarShiftCipher();

    public string EncryptMessage(string plainText, VigenereKey cipherKey)
    {
        // Nothing to encrypt.
        if (!DoesInputContainAlphabeticalCharacters(plainText)) { return string.Empty; }
        if (!DoesInputContainAlphabeticalCharacters(cipherKey.MemorableKey)) { return string.Empty; }

        var sb = new StringBuilder(string.Empty);

        var cipherAlphabets = GetCipherAlphabets(cipherKey.MemorableKey);
        var keyIndex = 0;

        foreach (var plainTextCharacter in plainText.ToUpper())
        {
            // Append special characters
            if (!IsAlphabeticalCharacter(plainTextCharacter))
            {
                sb.Append(plainTextCharacter);
                continue;
            }

            var multiAlphabetIndex = keyIndex % cipherKey.MemorableKey.Length;
            var currentAlphabet = cipherAlphabets
                .First(a => a.First().Equals(cipherKey.MemorableKey[multiAlphabetIndex]));

            sb.Append(currentAlphabet[Alphabet.IndexOf(plainTextCharacter)]);

            // Prevent keyIndex from overflowing.
            keyIndex = GetUpdatedKeyIndex(keyIndex, cipherKey.MemorableKey);
        }

        return sb.ToString();
    }

    public string DecryptMessage(string cipherText, VigenereKey cipherKey)
    {
        // Nothing to decrypt.
        if (!DoesInputContainAlphabeticalCharacters(cipherText)) { return string.Empty; }
            
        // Valid Vigenere cipher text (at least within this implementation) will contain only upper case letters
        // and special characters. Therefore any lower case letters render message invalid. Do not continue.
        if (cipherText.Where(char.IsLetter).Any(char.IsLower))
        {
            throw new InvalidOperationException("Invalid cipher text.");
        }
            
        var sb = new StringBuilder(string.Empty);
            
        var cipherAlphabets = GetCipherAlphabets(cipherKey.MemorableKey);
        var keyIndex = 0;

        foreach (var encryptedCharacter in cipherText)
        {
            // Append special characters.
            if (!IsAlphabeticalCharacter(encryptedCharacter))
            {
                sb.Append(encryptedCharacter);
                continue;
            }
                
            var multiAlphabetIndex = keyIndex % cipherKey.MemorableKey.Length;
            var currentAlphabet = cipherAlphabets
                .First(a => a.First().Equals(cipherKey.MemorableKey[multiAlphabetIndex]));
                
            sb.Append(currentAlphabet[Alphabet.IndexOf(encryptedCharacter)]);
                
            // Prevent keyIndex from overflowing.
            keyIndex = GetUpdatedKeyIndex(keyIndex, cipherKey.MemorableKey);
        }
            
        return sb.ToString().ToLower();
    }
    
    /// <summary>
        /// Determines whether the supplied cipher text/plain text contains alphabetical characters and therefore
        /// whether it's possible to encrypt or decrypt the input. 
        /// </summary>
        /// <param name="cipherOrPlainText">Cipher/Plain text input.</param>
        /// <returns><c>true</c>If input is alphabetical; otherwise <c>false</c>.</returns>
        private static bool DoesInputContainAlphabeticalCharacters(string cipherOrPlainText)
        {
            var regex = new Regex(RegexAlphabetPattern);
            var matches = regex.Matches(cipherOrPlainText, 0);
            return matches.Count != 0;
        }

        /// <summary>
        /// Determines whether the specified character is a member of the alphabet.
        /// </summary>
        /// <param name="character">Character to evaluate.</param>
        /// <returns><c>true</c> if character is within the alphabet; otherwise <c>false</c>.</returns>
        private static bool IsAlphabeticalCharacter(char character)
        {
            var regex = new Regex(RegexAlphabetPattern);
            var matches = regex.Matches(character.ToString(), 0);
            return matches.Count != 0;
        }

        /// <summary>
        /// Gets all the alphabets involved in the encryption/decryption for a specified key.
        /// </summary>
        /// <param name="memorableKey">Memorable cipher key.</param>
        /// <returns></returns>
        private static List<string> GetCipherAlphabets(string memorableKey)
        {
            return Alphabet
                .Select((t, i) =>
                    CaesarShiftCipher.EncryptMessage(Alphabet, new CaesarShiftCipherKey
                    {
                        Alphabet = Alphabet,
                        Shift = i
                    }))
                .Where(offset => memorableKey.Any(c => c.Equals(offset.First())))
                .ToList();
        }

        /// <summary>
        /// Increments or resets key index value, doing this will prevent this value from overflowing when
        /// encrypting/decrypting huge messages.
        /// </summary>
        /// <param name="currentKeyIndex"></param>
        /// <param name="memorableKey"></param>
        /// <returns></returns>
        private static int GetUpdatedKeyIndex(int currentKeyIndex, string memorableKey)
        {
            return currentKeyIndex == (memorableKey.Length - 1) ? 0 : currentKeyIndex + 1;
        }
}