using System.Collections.Generic;

namespace SimpleCryptography.CipherCrackers.FrequencyAnalysis
{
    public interface ICharFrequencyAnalyzer
    {
        CharacterFrequency GetSingleCharacterFrequency(char character, string sourceText);
        IEnumerable<CharacterFrequency> GetAllCharacterFrequencies(string sourceText);
        IEnumerable<CharacterFrequency> GetMultipleCharacterFrequencies(char[] characters, string sourceText);
    }
}