using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCryptography.CipherCrackers.FrequencyAnalysis
{
    public class CharFrequencyAnalyzer : ICharFrequencyAnalyzer
    {
        public CharFrequencyAnalyzer()
        {
        }
        
        public AnalysedCharacter GetSingleAnalyzedCharacter(char character, string sourceText)
        {
            var targetCharacter = new AnalysedCharacter(character);
            
            foreach (var currentCharacter in sourceText)
            {
                if (Equals(character, currentCharacter))
                {
                    targetCharacter.OccurenceCount++;
                }
            }
            
            targetCharacter.CalculateFrequency(sourceText.Length);
            
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
        /// <exception cref="ArgumentNullException"><paramref name="sourceText"/> is empty.</exception>
        public IEnumerable<AnalysedCharacter> GetAllAnalyzedCharacters(string sourceText)
        {
            if (string.IsNullOrEmpty(sourceText))
            {
                throw new ArgumentNullException("Cannot analyze contents of an empty input.");
            }
            
            var uniqueCharacters = new Dictionary<char, AnalysedCharacter>();

            // Collect all unique characters.
            foreach (var character in sourceText)
            {
                if (!uniqueCharacters.ContainsKey(character))
                {
                    // Collect all unique characters from source text
                    uniqueCharacters.Add(character, new AnalysedCharacter(character));
                }
                
                // Not inside an 'else' block, as newly added characters need an increment too.
                uniqueCharacters[character].OccurenceCount++;
            }
            
            // Once the source text is parsed, the frequency can be calculated, as the final
            // state of every character is now known.
            foreach (var character in uniqueCharacters)
            {
                character.Value.CalculateFrequency(sourceText.Length);
                yield return character.Value;
            }
        }

        public IEnumerable<AnalysedCharacter> GetMultipleAnalyzedCharacters(char[] characters, string sourceText)
        {
            if (characters.Length == 0) { throw new ArgumentNullException(nameof(characters)); }
            if (string.IsNullOrEmpty(sourceText)) { throw new ArgumentNullException(nameof(sourceText)); }
            
            var analyzedCharacters = characters.ToDictionary(selectedCharacter => 
                selectedCharacter, selectedCharacter => new AnalysedCharacter(selectedCharacter));

            foreach (var character in sourceText)
            {
                if (analyzedCharacters.ContainsKey(character))
                {
                    analyzedCharacters[character].OccurenceCount++;
                }
            }

            foreach (var character in analyzedCharacters)
            {
                character.Value.CalculateFrequency(sourceText.Length);
                yield return character.Value;
            }
        }
    }
}