using System.Collections.Generic;

namespace SimpleCryptography.CipherCrackers.FrequencyAnalysis
{
    public interface ICharFrequencyAnalyzer
    {
        AnalyzedCharacter GetSingleAnalyzedCharacter(char character, string sourceText);
        IEnumerable<AnalyzedCharacter> GetAllCharacterFrequencies(string sourceText);
        IEnumerable<AnalyzedCharacter> GetMultipleCharacterFrequencies(char[] characters, string sourceText);
    }
}