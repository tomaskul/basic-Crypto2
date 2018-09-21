namespace SimpleCryptography.Ciphers.Playfair_Cipher.Digrams
{
    public class Digram
    {
        /// <summary>
        /// First character within the digram.
        /// </summary>
        public char CharacterOne { get; private set; }

        /// <summary>
        /// Second character within the digram.
        /// </summary>
        public char CharacterTwo { get; private set; }

        public Digram(char characterOne, char characterTwo)
        {
            CharacterOne = characterOne;
            CharacterTwo = characterTwo;
        }

        public override string ToString()
        {
            return $"'{CharacterOne}{CharacterTwo}'"; 
        }
    }
}