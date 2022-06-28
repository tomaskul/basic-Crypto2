using System.Collections.Generic;
using SimpleCryptoLib.Ciphers.Playfair_Cipher.Digraphs;

namespace SimpleCryptoLib.Crackers.Playfair;

public interface IPlayfairAnalyser
{
    string ReplaceMostCommonDigraphs(string cipherText, IEnumerable<Digraph> replacementDigraphs);
}