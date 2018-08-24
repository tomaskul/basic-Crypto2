using System;
using System.Collections.Generic;

namespace SimpleCryptography.CipherCrackers.FrequencyAnalysis
{
    public class CharFrequencyAnalyzer : ICharFrequencyAnalyzer
    {
        private readonly int _frequencyDecimalPlaces = 0;
        private const int PercentageDelta = 100; // Makes reading the percentages easier.
        
        public CharFrequencyAnalyzer()
        {
        }

        public CharFrequencyAnalyzer(int frequencyDecimalPlaces)
        {
            if (frequencyDecimalPlaces <= 0)
            {
                throw new ArgumentOutOfRangeException("Invalid decimal place value.");
            }
            
            _frequencyDecimalPlaces = frequencyDecimalPlaces;
        }
        
        public AnalyzedCharacter GetSingleAnalyzedCharacter(char character, string sourceText)
        {
            var targetCharacter = new AnalyzedCharacter(character);
            
            foreach (var currentCharacter in sourceText)
            {
                if (Equals(character, currentCharacter))
                {
                    targetCharacter.OccurenceCount++;
                }
            }

            // Avoid division by zero exception.
            if (sourceText.Length != 0)
            {
                targetCharacter.Frequency = (Convert.ToDecimal(targetCharacter.OccurenceCount) 
                                / Convert.ToDecimal(sourceText.Length)) * PercentageDelta;

                // If rounding is enabled, round.
                if (_frequencyDecimalPlaces > 0)
                {
                    targetCharacter.Frequency = Math.Round(targetCharacter.Frequency, _frequencyDecimalPlaces);
                }
            }
            
            return targetCharacter;
        }

        /// <summary>
        /// Analyze all unique characters within the specified source text.
        /// </summary>
        /// <param name="sourceText"></param>
        /// <returns></returns>
        /// <remarks>
        /// This basic implementation should serve the purpose for time being. However, it is
        /// inefficient and processing time would grow very quickly with longer inputs. Ideally,
        /// the input should only be parsed once.
        /// </remarks>
        public IEnumerable<AnalyzedCharacter> GetAllCharacterFrequencies(string sourceText)
        {
            var uniqueCharacters = new List<char>();

            // Collect all unique characters.
            foreach (var character in sourceText)
            {
                if (!uniqueCharacters.Contains(character))
                {
                    uniqueCharacters.Add(character);
                }
            }

            // Analyze all characters.
            var analyzedCharacters = new List<AnalyzedCharacter>(uniqueCharacters.Count);
            foreach (var character in uniqueCharacters)
            {
                analyzedCharacters.Add(GetSingleAnalyzedCharacter(character, sourceText));
            }
            
            return analyzedCharacters;
        }

        public IEnumerable<AnalyzedCharacter> GetMultipleCharacterFrequencies(char[] characters, string sourceText)
        {
            throw new System.NotImplementedException();
        }
    }
}