using SimpleCryptography.CipherCrackers.FrequencyAnalysis;

namespace SimpleCryptography.CipherCrackers
{
    public abstract class AnalysedEntity
    {
        /// <summary>
        /// Number of times the entity appears within the source.
        /// </summary>
        public int OccurenceCount { get; set; }
        
        /// <summary>
        /// Percentage of the text that the character makes up.
        /// </summary>
        public decimal Frequency { get; set; }

        protected AnalysedEntity()
        {
            OccurenceCount = 0;
            Frequency = 0.0m;
        }
    }
}