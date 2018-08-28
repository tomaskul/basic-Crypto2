using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public class DigramGenerator : IDigramGenerator
    {
        private char DigramFillerCharacter { get; set; }

        private IEnumerable<Digram> Digrams { get; set; }

        public DigramGenerator(char digramFillerCharacter)
        {
            DigramFillerCharacter = digramFillerCharacter;
        }
        
        public IEnumerable<Digram> GetMessageDigrams(string plainText)
        {
            var digrams = new List<Digram>();
            var nullableDigram = new NullableDigram();;

            for (var i = 0; i < plainText.Length; i++)
            {
                // First character within diagram has to be the first one to be initialised in order to preserve
                // correct message order.
                if (nullableDigram.CharacterOne == null)
                {
                    nullableDigram.CharacterOne = plainText[i];

                    if (i + 1 == plainText.Length)
                    {
                        // End has been reached, and there are no other characters available to finish the digram.
                        // Use the default filler to account for this and pad the digram.
                        nullableDigram.CharacterTwo = DigramFillerCharacter;
                        digrams.Add(new Digram(nullableDigram.CharacterOne.Value, nullableDigram.CharacterTwo.Value));
                    }
                    else if (nullableDigram.CharacterOne == plainText[i + 1])
                    {
                        // End hasn't been reached so more digrams should be creared, however a digram cannot 
                        // consist of two identical characters, use filler to complete current digram.
                        nullableDigram.CharacterTwo = DigramFillerCharacter;
                        digrams.Add(new Digram(nullableDigram.CharacterOne.Value, nullableDigram.CharacterTwo.Value));
                        nullableDigram = new NullableDigram();;
                    }
                }
                else
                {
                    nullableDigram.CharacterTwo = plainText[i];
                    digrams.Add(new Digram(nullableDigram.CharacterOne.Value, nullableDigram.CharacterTwo.Value));
                    nullableDigram = new NullableDigram();;
                }
            }

            Digrams = digrams;
            return digrams;
        }

        /// <summary>
        /// Generates a string representing the last digram collection created by this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Digrams == null || !Digrams.Any()) { return string.Empty; }

            var sb = new StringBuilder(string.Empty);

            foreach (var digram in Digrams)
            {
                sb.Append(digram);
                if (digram != Digrams.Last())
                {
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }
    }
}