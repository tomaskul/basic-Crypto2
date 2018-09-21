using System.Collections.Generic;

namespace SimpleCryptography.CipherCrackers.FrequencyAnalysis
{
    public interface ICharFrequencyAnalyzer
    {
        AnalysedCharacter GetSingleAnalyzedCharacter(char character, string sourceText);
        IEnumerable<AnalysedCharacter> GetAllAnalyzedCharacters(string sourceText);
        IEnumerable<AnalysedCharacter> GetMultipleAnalyzedCharacters(char[] characters, string sourceText);
    }
}