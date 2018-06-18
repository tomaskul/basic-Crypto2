namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public class Digram
    {
        /// <summary>
        /// First character within the digram.
        /// </summary>
        public char? CharacterOne { get; set; } = null;

        /// <summary>
        /// Second character within the digram.
        /// </summary>
        public char? CharacterTwo { get; set; } = null;

        /// <summary>
        /// Determines whether the digram is complete (i.e. both characters set)
        /// </summary>
        /// <returns></returns>
        public bool IsComplete()
        {
            return CharacterOne != null && CharacterTwo != null;
        }
    }
}