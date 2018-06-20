namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public class CharacterPosition
    {
        public char Character { get; private set; }
        public int? Row { get; set; } = null;
        public int? Column { get; set; } = null;

        public CharacterPosition(char character)
        {
            Character = character;
        }
    }
}