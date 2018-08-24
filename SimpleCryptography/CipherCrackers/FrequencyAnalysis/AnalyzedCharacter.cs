namespace SimpleCryptography.CipherCrackers.FrequencyAnalysis
{
    public class AnalyzedCharacter
    {
        /// <summary>
        /// Character within source text.
        /// </summary>
        public char Character { get; set; }
        
        /// <summary>
        /// Number of times the character appears within source.
        /// </summary>
        public int OccurenceCount { get; set; }
        
        /// <summary>
        /// Percentage of the text that the character makes up.
        /// </summary>
        public double Frequency { get; set; }

        public AnalyzedCharacter()
        {
            OccurenceCount = 0;
            Frequency = 0.0;
        }

        public AnalyzedCharacter(char character) : this()
        {
            Character = character;
        }
    }
}