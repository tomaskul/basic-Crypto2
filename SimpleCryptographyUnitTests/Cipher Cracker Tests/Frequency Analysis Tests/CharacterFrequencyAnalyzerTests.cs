using System.Linq;
using NUnit.Framework;
using SimpleCryptography.CipherCrackers.FrequencyAnalysis;

namespace SimpleCryptographyUnitTests.Cipher_Cracker_Tests.Frequency_Analysis_Tests
{
    [TestFixture]
    public class CharacterFrequencyAnalyzerTests
    {   
        [Test]
        [TestCase('*', "", 0)]
        [TestCase('4', "asparagus", 0)]
        [TestCase('a', "aaabaaacaaadaaaa", 13)]
        [TestCase(' ', "quick brown fox counted the spaces", 5)]
        public void GetSingleAnalyzedCharacter_CorrectOccurenceCount(char character,
            string sourceText, int expectedCount)
        {
            var frequencyAnalyser = new CharFrequencyAnalyzer();
            
            var actual = frequencyAnalyser.GetSingleAnalyzedCharacter(character, sourceText);
            
            if (actual.OccurenceCount < 0)
            {
                Assert.Fail("Negative character occurence count");
            }
            
            Assert.AreEqual(expectedCount, actual.OccurenceCount);
        }

        [Test]
        [TestCase('.', "...", 1.000, 3)]
        [TestCase('!', "hi!", 0.333, 3)]
        [TestCase('o', "cloud", 0.200, 3)]
        [TestCase('a', "a quick brown fox jumped over a lazy dog", 0.075, 3)]
        public void GetSingleAnalyzedCharacter_CorrectFrequency(char character, string sourceText,
            decimal expectedFrequency, int decimalPlaceDelta)
        {
            var frequencyAnalyser = new CharFrequencyAnalyzer(decimalPlaceDelta);
            
            var actual = frequencyAnalyser.GetSingleAnalyzedCharacter(character, sourceText);
            
            Assert.AreEqual(expectedFrequency, actual.Frequency);
        }

        [Test]
        [TestCase(new[]{'a'}, "")]
        [TestCase(new[]{'a', 'b', 'z'}, "abbzzzz")]
        public void GetMultipleCharacterFrequencies_ValidInputs_AllCharactersPresent(char[] characters,
            string sourceText)
        {
            var frequencyAnalyser = new CharFrequencyAnalyzer();
            
            var frequencies = frequencyAnalyser.GetMultipleCharacterFrequencies(characters, sourceText);
            
            Assert.AreEqual(characters.Length, frequencies.Count());
        }
    }
}