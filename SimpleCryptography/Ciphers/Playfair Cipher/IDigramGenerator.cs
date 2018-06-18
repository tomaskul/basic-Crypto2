﻿using System.Collections.Generic;

namespace SimpleCryptography.Ciphers.Playfair_Cipher
{
    public interface IDigramGenerator
    {
        /// <summary>
        /// Generates an enumerable collection of digram objects from specified plain text.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>Collection of digram objects.</returns>
        IEnumerable<Digram> GetMessageDigram(string plainText);
    }
}