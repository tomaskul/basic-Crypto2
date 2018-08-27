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
        [TestCase('.', "...", 100.00, 3)]
        [TestCase('!', "hi!", 33.333, 3)]
        [TestCase('o', "cloud", 20.000, 3)]
        [TestCase('a', "a quick brown fox jumped over a lazy dog", 7.5, 1)]
        public void GetSingleAnalyzedCharacter_CorrectFrequency(char character, string sourceText,
            decimal expectedFrequency, int decimalPlaceDelta)
        {
            var frequencyAnalyser = new CharFrequencyAnalyzer(decimalPlaceDelta);
            
            var actual = frequencyAnalyser.GetSingleAnalyzedCharacter(character, sourceText);
            
            Assert.AreEqual(expectedFrequency, actual.Frequency);
        }        

        #endregion

        #region GetMultipleAnalyzedCharacters

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

        #endregion

        #region GetAllAnalyzedCharacters

        [Test]
        [TestCase("ax", new[]{'a', 'x'})]
        [TestCase("8888888888", new[]{'8'})]
        [TestCase("abcdeF", new[]{'a', 'b', 'c', 'd', 'e', 'F'})]
        public void GetAllCharacterFrequencies_FindsAllUniqueCharacters(string sourceText, char[] uniqueCharacters)
        {
            var frequencyAnalyser = new CharFrequencyAnalyzer();

            var output = frequencyAnalyser.GetAllCharacterFrequencies(sourceText);
            
            Assert.AreEqual(uniqueCharacters.Length, output.Count());
            
            foreach (var uniqueCharacter in uniqueCharacters)
            {
                if (!output.Any(ac => ac.Character.Equals(uniqueCharacter)))
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
        public void GetAllCharacterFrequencies_OccurenceCount_IsEqualToSourceTextLength(string sourceText)
        {
            var frequencyAnalyser = new CharFrequencyAnalyzer();

            var analyzedCharacters = frequencyAnalyser.GetAllCharacterFrequencies(sourceText);
            var totalOccurenceCount = analyzedCharacters.Sum(analyzedCharacter => analyzedCharacter.OccurenceCount);

            Assert.AreEqual(sourceText.Length, totalOccurenceCount);
        }

        [Test]
        public void GetAllCharacterFrequencies_EmptyInput_ThrowsArgumentNullException()
        {
            var frequencyAnalyzer = new CharFrequencyAnalyzer();

            //Assert.Throws<ArgumentNullException>(() => );
        }

        #endregion
        
    }
}