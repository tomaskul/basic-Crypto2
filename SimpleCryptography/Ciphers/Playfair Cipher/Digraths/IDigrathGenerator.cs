using System.Collections.Generic;

namespace SimpleCryptography.Ciphers.Playfair_Cipher.Digraths
{
    public interface IDigrathGenerator
    {
        /// <summary>
        /// Generates an enumerable collection of digrath objects from specified plain text.
        /// </summary>
        /// <param name="plainText">Plain text message.</param>
        /// <returns>Collection of digraths.</returns>
        IEnumerable<Digraph> GetMessageDigraths(string plainText);
    }
}