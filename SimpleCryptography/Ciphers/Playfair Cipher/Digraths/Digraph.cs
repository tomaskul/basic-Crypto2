namespace SimpleCryptography.Ciphers.Playfair_Cipher.Digraths
{
    public class Digraph
    {
        /// <summary>
        /// First character within the digrath.
        /// </summary>
        public char CharacterOne { get; private set; }

        /// <summary>
        /// Second character within the digrath.
        /// </summary>
        public char CharacterTwo { get; private set; }

        public Digraph(char characterOne, char characterTwo)
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