using NUnit.Framework;
using SimpleCryptoLib.Ciphers.Playfair_Cipher.Digraphs;

namespace SimpleCryptoUnitTests.CipherTests.Playfair_Cipher;

[TestFixture]
public class DigramGeneratorTests
{
    private static readonly IDigrathGenerator DigrathGenerator = new DigrathGenerator('X');
        
    [Test]
    [TestCase("X", "XX")]
    [TestCase("LOL", "LO LX")]
    [TestCase("AHEEL", "AH EX EL")]
    [TestCase("NOOO", "NO OX OX")]
    [TestCase("MEETMEATHAMMERSMITHBRIDGETONIGHT", "ME ET ME AT HA MX ME RS MI TH BR ID GE TO NI GH TX")]
    public void GenerateDigram_ValidInputs(string message, string toStringOutput)
    {
        DigrathGenerator.GetMessageDigraths(message);
        Assert.AreEqual(toStringOutput, DigrathGenerator.ToString());
    }
}