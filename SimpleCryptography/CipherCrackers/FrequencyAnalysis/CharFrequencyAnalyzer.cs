using System;
using System.Collections.Generic;

namespace SimpleCryptography.CipherCrackers.FrequencyAnalysis
{
    public class CharFrequencyAnalyzer : ICharFrequencyAnalyzer
    {
        public CharFrequencyAnalyzer()
        {
        }
        
        public AnalyzedCharacter GetSingleAnalyzedCharacter(char character, string sourceText)
        {
            var characterFrequency = new AnalyzedCharacter(character);
            
            foreach (var currentCharacter in sourceText)
            {
                if (Equals(character, currentCharacter))
                {
                    characterFrequency.OccurenceCount++;
                }
            }

            // Avoid division by zero exception.
            if (sourceText.Length != 0)
            {
                double frequency = characterFrequency.OccurenceCount / sourceText.Length;
                characterFrequency.Frequency = Math.Round(frequency, 5);
            }
            
            return characterFrequency;
        }

        public IEnumerable<AnalyzedCharacter> GetAllCharacterFrequencies(string sourceText)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AnalyzedCharacter> GetMultipleCharacterFrequencies(char[] characters, string sourceText)
        {
            throw new System.NotImplementedException();
        }
    }
}