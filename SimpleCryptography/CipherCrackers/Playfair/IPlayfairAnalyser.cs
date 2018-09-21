namespace SimpleCryptography.CipherCrackers.Playfair
{
    public interface IPlayfairAnalyser
    {
        string ReplaceMostCommonDigrams(string cipherText, ReplacementDigrams replacementDigrams);
    }
}