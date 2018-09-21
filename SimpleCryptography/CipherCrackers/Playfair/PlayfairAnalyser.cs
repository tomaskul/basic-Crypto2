using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCryptography.Ciphers.Playfair_Cipher;
using SimpleCryptography.Ciphers.Playfair_Cipher.Digrams;

namespace SimpleCryptography.CipherCrackers.Playfair
{
    public class PlayfairAnalyser : IPlayfairAnalyser
    {   
        public string ReplaceMostCommonDigrams(string cipherText, ReplacementDigrams replacementDigrams)
        {
            if (!PlayfairUtil.IsCipherTextValid(cipherText)) { throw new ArgumentOutOfRangeException(nameof(cipherText)); }
            if (replacementDigrams == null || !replacementDigrams.Digrams.Any()) { throw new ArgumentOutOfRangeException(nameof(replacementDigrams));}

            var replacementDigramss = new List<Digram>(replacementDigrams.Digrams.Count());
            
            
            var analysedDigrams = new Dictionary<Digram, AnalysedDigram>();
            for (var i = 0; i < cipherText.Length; i += 2)
            {
                var currentDigram = new Digram(cipherText[i], cipherText[i + 1]);
                
                if (analysedDigrams.ContainsKey(currentDigram))
                {
                    // Update the occurance count if digram has already been observed.
                    analysedDigrams[currentDigram].OccurenceCount++;
                }
                else
                {
                    // Add new digram to the dictionary and count the initial occurance.
                    analysedDigrams.Add(currentDigram, new AnalysedDigram
                    {
                        Digram = currentDigram,
                        OccurenceCount = 1
                    });
                    
                }
            }

            var cipherTextDigramCount = cipherText.Length / 2;
            foreach (var analysedDigram in analysedDigrams)
            {
                analysedDigram.Value.CalculateFrequency(cipherTextDigramCount);
            }
            
            throw new System.NotImplementedException();
        }
    }
}