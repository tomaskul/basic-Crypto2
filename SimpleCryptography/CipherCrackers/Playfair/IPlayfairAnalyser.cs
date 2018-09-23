using System.Collections.Generic;
using SimpleCryptography.Ciphers.Playfair_Cipher.Digraths;

namespace SimpleCryptography.CipherCrackers.Playfair
{
    public interface IPlayfairAnalyser
    {
        string ReplaceMostCommonDigraphs(string cipherText, IEnumerable<Digraph> replacementDigraphs);
    }
}