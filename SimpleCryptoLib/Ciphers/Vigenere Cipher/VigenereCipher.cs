using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SimpleCryptoLib.Ciphers.Caesar_Shift_Cipher;

namespace SimpleCryptoLib.Ciphers.Vigenere_Cipher;

/// <summary>Implementation of <see cref="IVigenereCipher"/>.</summary>
public class VigenereCipher : IVigenereCipher
{
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string RegexAlphabetPattern = "[a-zA-Z]";
    private static readonly CaesarShiftCipher CaesarShiftCipher = new CaesarShiftCipher();

    public string EncryptMessage(string plainText, VigenereKey cipherKey)
    {
        // Nothing to encrypt.
        if (!DoesInputContainAlphabeticalCharacters(plainText))
            return string.Empty;

        if (!DoesInputContainAlphabeticalCharacters(cipherKey.MemorableKey))
            return string.Empty;

        var sb = new StringBuilder(string.Empty);

        var cipherAlphabets = GetCipherAlphabets(cipherKey.MemorableKey);
        var keyIndex = 0;

        foreach (var plainTextCharacter in plainText.ToUpper())
        {
            // Append special characters
            if (!DoesInputContainAlphabeticalCharacters(plainTextCharacter.ToString()))
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
        if (!DoesInputContainAlphabeticalCharacters(cipherText))
            return string.Empty;

        // Valid Vigenere cipher text (at least within this implementation) will contain only upper case letters
        // and special characters. Therefore any lower case letters render message invalid. Do not continue.
        if (cipherText.Where(char.IsLetter).Any(char.IsLower))
            throw new InvalidOperationException("Invalid cipher text.");

        var sb = new StringBuilder(string.Empty);

        var cipherAlphabets = GetCipherAlphabets(cipherKey.MemorableKey);
        var keyIndex = 0;

        foreach (var encryptedCharacter in cipherText)
        {
            // Append special characters.
            if (!DoesInputContainAlphabeticalCharacters(encryptedCharacter.ToString()))
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
    /// Determines whether the supplied <paramref name="input"/> contains alphabetical character(s).
    /// </summary>
    /// <param name="input">Input to analyse.</param>
    /// <returns><c>true</c>If input is alphabetical; otherwise <c>false</c>.</returns>
    private static bool DoesInputContainAlphabeticalCharacters(string input)
    {
        var regex = new Regex(RegexAlphabetPattern);
        var matches = regex.Matches(input, 0);
        return matches.Count != 0;
    }

    /// <summary>
    /// Gets all the alphabets involved in the encryption/decryption for a specified key.
    /// </summary>
    /// <param name="memorableKey">Memorable cipher key.</param>
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
        => currentKeyIndex == (memorableKey.Length - 1) 
            ? 0 
            : currentKeyIndex + 1;
}