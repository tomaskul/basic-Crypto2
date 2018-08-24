using System.Linq;
using NUnit.Framework;
using SimpleCryptography.CipherCrackers.FrequencyAnalysis;

namespace SimpleCryptographyUnitTests.Cipher_Cracker_Tests.Frequency_Analysis_Tests
{
    [TestFixture]
    public class CharacterFrequencyAnalyzerTests
    {
        private ICharFrequencyAnalyzer _frequencyAnalyser = new CharFrequencyAnalyzer();
        
        [Test]
        [TestCase('*', "", 0)]
        [TestCase('4', "asparagus", 0)]
        [TestCase('a', "aaabaaacaaadaaaa", 13)]
        [TestCase(' ', "quick brown fox counted the spaces", 5)]
        public void GetSingleAnalyzedCharacter_CorrectOccurenceCount(char character,
            string sourceText, int expectedCount)
        {
            var actual = _frequencyAnalyser.GetSingleAnalyzedCharacter(character, sourceText);

            if (actual.OccurenceCount < 0)
            {
                Assert.Fail("Negative character occurence count");
            }
            
            Assert.AreEqual(expectedCount, actual.OccurenceCount);
        }

        [Test]
        [TestCase('.', "...", 100.000, 3)]
        [TestCase('!', "hi!", 33.333, 3)]
        public void GetSingleAnalyzedCharacter_CorrectFrequency(char character, string sourceText,
            double expectedFrequency, double discrepencyDelta)
        {
            var actual = _frequencyAnalyser.GetSingleAnalyzedCharacter(character, sourceText);
            
            Assert.AreEqual(expectedFrequency, actual.Frequency, discrepencyDelta);
        }

        [Test]
        [TestCase(new[]{'a'}, "")]
        [TestCase(new[]{'a', 'b', 'z'}, "abbzzzz")]
        public void GetMultipleCharacterFrequencies_ValidInputs_AllCharactersPresent(char[] characters,
            string sourceText)
        {
            var frequencies = _frequencyAnalyser.GetMultipleCharacterFrequencies(characters, sourceText);
            
            Assert.AreEqual(characters.Length, frequencies.Count());
        }
    }
}