namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public class CharacterPosition
    {
        /// <summary>
        /// Character being processed.
        /// </summary>
        public char Character { get; private set; }
        
        /// <summary>
        /// Character row value.
        /// </summary>
        public int? Row { get; set; } = null;
        
        /// <summary>
        /// Character column value.
        /// </summary>
        public int? Column { get; set; } = null;

        public CharacterPosition(char character)
        {
            Character = character;
        }
    }
}