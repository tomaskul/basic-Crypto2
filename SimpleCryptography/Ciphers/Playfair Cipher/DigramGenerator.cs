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

        /// <summary>
        /// Generates an enumerable collection of digram objects from specified plain text.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public IEnumerable<Digram> GetMessageDigram(string plainText)
        {
            var digrams = new List<Digram>();
            var digram = new Digram();

            for (var i = 0; i < plainText.Length; i++)
            {
                // First character within diagram has to be the first one to be initialised in order to preserve
                // correct message order.
                if (digram.CharacterOne == null)
                {
                    digram.CharacterOne = plainText[i];

                    if (i + 1 == plainText.Length)
                    {
                        // End has been reached, and there are no other characters available to finish the digram.
                        // Use the default filler to account for this and pad the digram.
                        digram.CharacterTwo = DigramFillerCharacter;
                        digrams.Add(digram);
                    }
                    else if (digram.CharacterOne == plainText[i + 1])
                    {
                        // End hasn't been reached so more digrams should be creared, however a digram cannot 
                        // consist of two identical characters, use filler to complete current digram.
                        digram.CharacterTwo = DigramFillerCharacter;
                        digrams.Add(digram);
                        digram = new Digram();
                    }
                }
                else
                {
                    digram.CharacterTwo = plainText[i];
                    digrams.Add(digram);
                    digram = new Digram();
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