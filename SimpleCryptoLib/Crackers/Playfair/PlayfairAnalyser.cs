using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCryptoLib.Ciphers.Playfair_Cipher;
using SimpleCryptoLib.Ciphers.Playfair_Cipher.Digraphs;

namespace SimpleCryptoLib.Crackers.Playfair;

public class PlayfairAnalyser : IPlayfairAnalyser
{
    private readonly IDigrathGenerator _digraphGenerator;
        
        public PlayfairAnalyser(IDigrathGenerator digraphGenerator)
        {
            _digraphGenerator = digraphGenerator;
        }

        public string ReplaceMostCommonDigraphs(string cipherText, IEnumerable<Digraph> replacementDigraphs)
        {
            if (!PlayfairUtil.IsValidCipherText(cipherText)) { throw new ArgumentOutOfRangeException(nameof(cipherText)); }
            replacementDigraphs = replacementDigraphs as Digraph[] ?? replacementDigraphs.ToArray();
            if (replacementDigraphs == null || !replacementDigraphs.Any()) { throw new ArgumentOutOfRangeException(nameof(replacementDigraphs)); }

            // Get all digraths and count their occurance count
            var analysedDigraphs = new Dictionary<string, AnalysedDigrath>();

            var cipherTextDigraphs = _digraphGenerator.GetCipherTextDigraphs(cipherText);
            foreach (var digraph in cipherTextDigraphs)
            {
                if (analysedDigraphs.ContainsKey(digraph.ToString()))
                {
                    // Update the occurance count if digraph has already been observed.
                    analysedDigraphs[digraph.ToString()].OccurenceCount++;
                }
                else
                {
                    // Add new digraph to the dictionary and count the initial occurance.
                    analysedDigraphs.Add(digraph.ToString(), new AnalysedDigrath
                    {
                        Digraph = digraph,
                        OccurenceCount = 1
                    });
                }
            }

            // Commentted out as this metric isn't currently needed. Left within the function for easy future
            // implementation.
            /* Calculate digram frequency & order digrams by their occurance.
            var cipherTextDigramCount = cipherText.Length / DigramDenominator;
            foreach (var analysedDigram in analysedDigrams)
            {
                analysedDigram.Value.CalculateFrequency(cipherTextDigramCount);
            }
            */
            
            // Orders digraphs by descending order of occurance count and extracts amount to match method input.
            var orderedAnalysedDigraphs = analysedDigraphs
                .OrderByDescending(kv => kv.Value.OccurenceCount)
                .Take(replacementDigraphs.Count())
                .ToArray();

            // Create a replacement map for the most common digraphs.
            var digraphReplacementMap = new Dictionary<string, Digraph>();
            for (var i = 0; i < replacementDigraphs.Count(); i++)
            {
                digraphReplacementMap.Add(orderedAnalysedDigraphs[i].Key, replacementDigraphs.ElementAt(i));
            }
            
            // Reconstruct the cipher text whilst replacing the most common digraphs within original cipher text with
            // most frequent digraphs in the assumed language.
            var sb = new StringBuilder(string.Empty);
            foreach (var digraph in cipherTextDigraphs)
            {
                sb.Append(digraphReplacementMap.ContainsKey(digraph.ToString())
                    ? digraphReplacementMap[digraph.ToString()]
                    : digraph);
            }
            return sb.ToString();
        }
}