﻿using System;
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

            targetCharacter.Frequency = GetCharacterFrequency(targetCharacter.OccurenceCount, sourceText.Length);
            
            return targetCharacter;
        }

        /// <summary>
        /// Calculates the percentage of total character count, that a single character composes.
        /// </summary>
        /// <param name="occurenceCount">Number of character occurances within source text.</param>
        /// <param name="totalCharacterCount">Length of source text.</param>
        /// <returns>Frequency percentage</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>totalCharacterCount</c> is below or
        /// equal to zero.</exception>
        private decimal GetCharacterFrequency(int occurenceCount, int totalCharacterCount)
        {
            // Avoid division by zero exception
            if (totalCharacterCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalCharacterCount));
            }

            var frequency = (Convert.ToDecimal(occurenceCount) / Convert.ToDecimal(totalCharacterCount))
                            * PercentageDelta;

            if (_frequencyDecimalPlaces > 0)
            {
                frequency = Math.Round(frequency, _frequencyDecimalPlaces);
            }

            return frequency;
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
        /// <exception cref="ArgumentNullException"><paramref name="sourceText"/> is empty.</exception>
        public IEnumerable<AnalyzedCharacter> GetAllCharacterFrequencies(string sourceText)
        {
            if (string.IsNullOrEmpty(sourceText))
            {
                throw new ArgumentNullException("Cannot analyze contents of an empty input.");
            }
            
            var uniqueCharacters = new Dictionary<char, AnalyzedCharacter>();

            // Collect all unique characters.
            foreach (var character in sourceText)
            {
                if (!uniqueCharacters.ContainsKey(character))
                {
                    // Collect all unique characters from source text
                    uniqueCharacters.Add(character, new AnalyzedCharacter(character));
                }
                
                // Not inside an 'else' block, as newly added characters need an increment too.
                uniqueCharacters[character].OccurenceCount++;
            }
            
            // Once the source text is parsed, the frequency can be calculated, as the final
            // state of every character is now known.
            foreach (var character in uniqueCharacters)
            {
                character.Value.Frequency = GetCharacterFrequency(character.Value.OccurenceCount, sourceText.Length);
                yield return character.Value;
            }
        }

        public IEnumerable<AnalyzedCharacter> GetMultipleCharacterFrequencies(char[] characters, string sourceText)
        {
            throw new System.NotImplementedException();
        }
    }
}