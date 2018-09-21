using System.Collections.Generic;

namespace SimpleCryptography.Ciphers.Playfair_Cipher.Digraths
{
    public interface IDigrathGenerator
    {
        /// <summary>
        /// Generates a collection of digraths from specified plain text.
        /// </summary>
        /// <param name="plainText">Plain text message.</param>
        /// <returns>Digraths.</returns>
        IEnumerable<Digraph> GetMessageDigraths(string plainText);

        /// <summary>
        /// Generates a collection of digraths from specified, valid cipher text.
        /// </summary>
        /// <param name="cipherText">Valid cipher text.</param>
        /// <returns>Digraths.</returns>
        IEnumerable<Digraph> GetCipherTextDigraphs(string cipherText);
    }
}