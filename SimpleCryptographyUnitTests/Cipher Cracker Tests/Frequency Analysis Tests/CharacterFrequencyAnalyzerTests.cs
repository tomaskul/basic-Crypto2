using System;
using System.Linq;
using NUnit.Framework;
using SimpleCryptography.CipherCrackers.FrequencyAnalysis;

namespace SimpleCryptographyUnitTests.Cipher_Cracker_Tests.Frequency_Analysis_Tests
{
    [TestFixture]
    public class CharacterFrequencyAnalyzerTests
    {
        #region GetSingleAnalyzedCharacter

        [Test]
        [TestCase('*', "")]
        public void GetSingleAnalyzedCharacter_NoContent_ThrowsArgumentOutOfRange(char character,
            string sourceText)
        {
            var frequencyAnalyser = new CharFrequencyAnalyzer();
            
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                frequencyAnalyser.GetSingleAnalyzedCharacter(character, sourceText);
            });
        }
        
        [Test]
        [TestCase('4', "asparagus", 0)]
        [TestCase('a', "aaabaaacaaadaaaa", 13)]
        [TestCase(' ', "quick brown fox counted the spaces", 5)]
        public void GetSingleAnalyzedCharacter_HasContent_IsEqualToExpectedLength(char character,
            string sourceText, int expectedCount)
        {
            var frequencyAnalyser = new CharFrequencyAnalyzer();
            var analyzedCharacter = frequencyAnalyser.GetSingleAnalyzedCharacter(character, sourceText);
            
            if (analyzedCharacter.OccurenceCount < 0)
            {
                Assert.Fail("Negative character occurence count");
            }
            
            Assert.AreEqual(expectedCount, analyzedCharacter.OccurenceCount);
        }

        [Test]
        [TestCase('.', "...", 100.00, 3)]
        [TestCase('!', "hi!", 33.333, 3)]
        [TestCase('o', "cloud", 20.000, 3)]
        [TestCase('a', "a quick brown fox jumped over a lazy dog", 7.5, 1)]
        public void GetSingleAnalyzedCharacter_CorrectFrequency(char character, string sourceText,
            decimal expectedFrequency, int decimalPlaceDelta)
        {
            var frequencyAnalyser = new CharFrequencyAnalyzer();
            
            var actual = frequencyAnalyser.GetSingleAnalyzedCharacter(character, sourceText);
            
            Assert.AreEqual(expectedFrequency, decimal.Round(actual.Frequency, decimalPlaceDelta));
        }        

        #endregion

        #region GetMultipleAnalyzedCharacters

        [Test]
        [TestCase(new[]{'a', 'b', 'z'}, "abbzzzz")]
        public void GetMultipleAnalyzedCharacters_HasContent_AllCharactersPresent(char[] characters,
            string sourceText)
        {
            var frequencyAnalyser = new CharFrequencyAnalyzer();
            var analyzedCharacters = frequencyAnalyser.GetMultipleAnalyzedCharacters(characters, sourceText).ToArray();
            
            Assert.AreEqual(characters.Length, analyzedCharacters.Length);

            foreach (var character in characters)
            {
                if (!analyzedCharacters.Any(ac => ac.Character.Equals(character)))
                {
                    Assert.Fail($"Character '{character}' was not found in the analyzed set.");
                }
            }
            
            Assert.Pass();
        }

        #endregion

        #region GetAllAnalyzedCharacters

        [Test]
        [TestCase("ax", new[]{'a', 'x'})]
        [TestCase("8888888888", new[]{'8'})]
        [TestCase("abcdeF", new[]{'a', 'b', 'c', 'd', 'e', 'F'})]
        public void GGetAllAnalyzedCharacters_FindsAllUniqueCharacters(string sourceText, char[] uniqueCharacters)
        {
            var frequencyAnalyser = new CharFrequencyAnalyzer();
            var analyzedCharacters = frequencyAnalyser.GetAllAnalyzedCharacters(sourceText).ToArray();
            
            Assert.AreEqual(uniqueCharacters.Length, analyzedCharacters.Count());
            
            foreach (var uniqueCharacter in uniqueCharacters)
            {
                if (!analyzedCharacters.Any(ac => ac.Character.Equals(uniqueCharacter)))
                {
                    Assert.Fail($"Character: '{uniqueCharacter}' was not found in the output set.");
                }
            }
            
            Assert.Pass();
        }

        [Test]
        [TestCase("a")]
        [TestCase("lkfnldsfnalsf")]
        [TestCase("NOw let's try this with a longer input.")]
        public void GetAllAnalyzedCharacters_OccurenceCount_IsEqualToSourceTextLength(string sourceText)
        {
            var frequencyAnalyser = new CharFrequencyAnalyzer();
            var analyzedCharacters = frequencyAnalyser.GetAllAnalyzedCharacters(sourceText);
            
            var totalOccurenceCount = analyzedCharacters.Sum(analyzedCharacter => analyzedCharacter.OccurenceCount);

            Assert.AreEqual(sourceText.Length, totalOccurenceCount);
        }

        [Test]
        public void GetAllAnalyzedCharacters_EmptyInput_ThrowsArgumentNullException()
        {
            var frequencyAnalyzer = new CharFrequencyAnalyzer();

            Assert.Throws<ArgumentNullException>(() => { frequencyAnalyzer.GetAllAnalyzedCharacters(string.Empty); });
        }

        #endregion
        
    }
}