using System;
using System.Collections.Generic;
using System.Diagnostics;
using SimpleCryptography.Ciphers.Caesar_Shift_Cipher;
using SimpleCryptography.Ciphers.Monoalphabetic_Substitution_Cipher;

namespace SimpleCryptographyRunner
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*
            var msc = new MonoalphabeticSubstitutionCipher();
            var message = "CJPXGGC";
            var key = new MonoalphabeticSubstitutionKey(new Dictionary<char, char>()
            {
                { 'c', 'A'}, { 'i', 'B'}, { 'o', 'C'}, { 'v', 'D'}, { 'y', 'E'},
                { 'b', 'F'}, { 'l', 'G'}, { 'k', 'H'}, { 'f', 'I'}, { 't', 'J'},
                { 'q', 'K'}, { 'm', 'L'}, { 'a', 'M'}, { 'd', 'N'}, { 'z', 'O'},
                { 'h', 'P'}, { 'p', 'Q'}, { 's', 'R'}, { 'j', 'S'}, { 'n', 'T'},
                { 'u', 'U'}, { 'r', 'V'}, { 'g', 'W'}, { 'e', 'X'}, { 'w', 'Y'},
                { 'x', 'Z'}, { ' ', ' '}, { '.', '.'}, { '\'','\''}, { ',', ','},
                { ';', ';'}
            });
            
            Console.WriteLine(msc.DecryptMessage(message, key));
            */

            /*
            var csc = new CaesarShiftCipher();
            var message = "faber est suae quisque fortunae appius claudius caecus dictum est";
            
            Console.WriteLine($"Plaintext message:\t{message}\n");
            Console.WriteLine($"Encrypted message:\t{csc.EncryptMessage(message, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 7)}");
            */
            
            Console.ReadKey();
        }
    }
}