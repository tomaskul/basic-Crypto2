using NUnit.Framework;
using SimpleCryptography.Ciphers.Playfair_Cipher;

namespace SimpleCryptographyUnitTests.Cipher_Tests.Playfair_Cipher
{
    [TestFixture]
    public class DigramGeneratorTests
    {
        private static readonly IDigramGenerator DigramGenerator = new DigramGenerator('X');
        
        [Test]
        [TestCase("X", "'XX'")]
        [TestCase("LOL", "'LO' 'LX'")]
        [TestCase("AHEEL", "'AH' 'EX' 'EL'")]
        [TestCase("NOOO", "'NO' 'OX' 'OX'")]
        [TestCase("MEETMEATHAMMERSMITHBRIDGETONIGHT", "'ME' 'ET' 'ME' 'AT' 'HA' 'MX' 'ME' 'RS' 'MI' 'TH' 'BR' 'ID' 'GE' 'TO' 'NI' 'GH' 'TX'")]
        public void GenerateDigram_ValidInputs(string message, string toStringOutput)
        {
            DigramGenerator.GetMessageDigram(message);
            Assert.AreEqual(toStringOutput, DigramGenerator.ToString());
        }
    }
}