using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCryptography.Ciphers.Playfair_Cipher.Digraths
{
    public class DigrathGenerator : IDigrathGenerator
    {
        private char DigrathFillerCharacter { get; set; }
        private IEnumerable<Digraph> Digraths { get; set; }

        public DigrathGenerator(char digrathFillerCharacter)
        {
            DigrathFillerCharacter = digrathFillerCharacter;
        }

        public IEnumerable<Digraph> GetMessageDigraths(string plainText)
        {
            var digraths = new List<Digraph>();
            var nullableDigrath = new NullableDigrath();

            for (var i = 0; i < plainText.Length; i++)
            {
                // First character within diagrath has to be the first one to be initialised in order to preserve
                // correct message order.
                if (nullableDigrath.CharacterOne == null)
                {
                    nullableDigrath.CharacterOne = plainText[i];

                    if (i + 1 == plainText.Length)
                    {
                        // End has been reached, and there are no other characters available to finish the digrath.
                        // Use the default filler to account for this and pad the digrath.
                        nullableDigrath.CharacterTwo = DigrathFillerCharacter;
                        digraths.Add(new Digraph(nullableDigrath.CharacterOne.Value, nullableDigrath.CharacterTwo.Value));
                    }
                    else if (nullableDigrath.CharacterOne == plainText[i + 1])
                    {
                        // End hasn't been reached so more digraths should be creared, however a digrath cannot 
                        // consist of two identical characters, use filler to complete current digrath.
                        nullableDigrath.CharacterTwo = DigrathFillerCharacter;
                        digraths.Add(new Digraph(nullableDigrath.CharacterOne.Value, nullableDigrath.CharacterTwo.Value));
                        nullableDigrath = new NullableDigrath();
                    }
                }
                else
                {
                    nullableDigrath.CharacterTwo = plainText[i];
                    digraths.Add(new Digraph(nullableDigrath.CharacterOne.Value, nullableDigrath.CharacterTwo.Value));
                    nullableDigrath = new NullableDigrath();
                }
            }

            Digraths = digraths;
            return digraths;
        }

        public IEnumerable<Digraph> GetCipherTextDigraphs(string cipherText)
        {
            if (!PlayfairUtil.IsValidCipherText(cipherText))
            {
                throw new ArgumentException("Invalid cipher text.");
            }

            var digrapths = new List<Digraph>();
            for (var i = 0; i < cipherText.Length; i += PlayfairUtil.DigrathDenominator)
            {
                digrapths.Add(new Digraph(cipherText[i], cipherText[i + 1]));
            }

            Digraths = digrapths;
            return digrapths;
        }

        /// <summary>
        /// Generates a string representing the last digrath collection created by this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Digraths == null || !Digraths.Any()) { return string.Empty; }

            var sb = new StringBuilder(string.Empty);

            foreach (var digraph in Digraths)
            {
                sb.Append(digraph);
                if (digraph != Digraths.Last()) { sb.Append(" "); }
            }

            return sb.ToString();
        }
    }
}