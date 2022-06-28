using System.Collections.Generic;

namespace SimpleCryptoLib.Crackers.Frequency_Analysis;

public interface ICharFrequencyAnalyzer
{
    AnalysedCharacter GetSingleAnalyzedCharacter(char character, string sourceText);
    IEnumerable<AnalysedCharacter> GetAllAnalyzedCharacters(string sourceText);
    IEnumerable<AnalysedCharacter> GetMultipleAnalyzedCharacters(char[] characters, string sourceText);
}