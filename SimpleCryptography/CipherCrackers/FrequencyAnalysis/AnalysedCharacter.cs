namespace SimpleCryptography.CipherCrackers.FrequencyAnalysis
{
    public class AnalysedCharacter : AnalysedEntity
    {
        /// <summary>
        /// Character within source text.
        /// </summary>
        public char Character { get; set; }
        
        public AnalysedCharacter(char character)
        {
            Character = character;
        }
    }
}