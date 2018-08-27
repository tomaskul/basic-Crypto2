using System.Collections.Generic;

namespace SimpleCryptography.CipherCrackers.FrequencyAnalysis
{
    public interface ICharFrequencyAnalyzer
    {
        AnalyzedCharacter GetSingleAnalyzedCharacter(char character, string sourceText);
        IEnumerable<AnalyzedCharacter> GetAllAnalyzedCharacters(string sourceText);
        IEnumerable<AnalyzedCharacter> GetMultipleAnalyzedCharacters(char[] characters, string sourceText);
    }
}