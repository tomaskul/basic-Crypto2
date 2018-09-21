namespace SimpleCryptography.CipherCrackers.FrequencyAnalysis
{
    public class AnalyzedCharacter : AnalysedEntity
    {
        /// <summary>
        /// Character within source text.
        /// </summary>
        public char Character { get; set; }
        
        public AnalyzedCharacter(char character)
        {
            Character = character;
        }
    }
}