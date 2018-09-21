namespace SimpleCryptography.Ciphers.Playfair_Cipher.Digraths
{
    public class NullableDigrath
    {
        /// <summary>
        /// First character within the digrath.
        /// </summary>
        public char? CharacterOne { get; set; } = null;

        /// <summary>
        /// Second character within the digrath.
        /// </summary>
        public char? CharacterTwo { get; set; } = null;

        public override string ToString()
        {
            if (IsComplete()) { return $"'{CharacterOne}{CharacterTwo}'"; }

            return CharacterOne != null ? $"'{CharacterOne}_'" : $"'_{CharacterTwo}'";
        }
        
        private bool IsComplete()
        {
            return CharacterOne != null && CharacterTwo != null;
        }
    }
}