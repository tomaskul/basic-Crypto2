using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using SimpleCryptography.CipherCrackers.Playfair;
using SimpleCryptography.Ciphers.Playfair_Cipher;
using SimpleCryptography.Ciphers.Playfair_Cipher.Digraths;
using SimpleCryptography.Ciphers.Playfair_Cipher.Key_Management;

namespace SimpleCryptographyRunner
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            var playfair = new PlayfairCipher(new DigrathGenerator('X'), new PlayfairKeyManagement());
            
            /*var a = playfair.EncryptMessage("message", "ZYCDEFGHIJKLMNOPRSTUVWXBA");*/
            /*
            Console.WriteLine();
            
            Console.WriteLine(playfair.EncryptMessage("Hide the gold in the tree stump", "playfair example"));
            */


            var replacementDigraphs = new List<Digraph>
            {
                new Digraph('t', 'h'),
                new Digraph('h', 'e'),
                new Digraph('a', 'n'),
                new Digraph('i', 'n'),
                new Digraph('e', 'r'),
                new Digraph('r', 'e'),
                new Digraph('e', 's')
            };
            
            var replacementDigraphs2 = new List<Digraph>
            {
                new Digraph('h', 'e'),
                new Digraph('t', 'h'),
                new Digraph('i', 'n'),
                //new Digraph('e', 'r'),
                new Digraph('a', 'n'),
                new Digraph('r', 'e'),
                new Digraph('e', 's'),
                new Digraph('s', 't'),
                new Digraph('e', 'r'),
                new Digraph('t', 'e'),
                new Digraph('e', 'n'),
                new Digraph('a', 't')
            };
            
            var replacementDigraphs3 = new List<Digraph>
            {
                new Digraph('h', 'e'),
                new Digraph('t', 'h'),
                new Digraph('i', 'n'),
                new Digraph('r', 'e'),
                new Digraph('a', 'n'),
                new Digraph('e', 's'),
                new Digraph('s', 't'),
                new Digraph('t', 'e'),
                new Digraph('e', 'r'),
                new Digraph('e', 'n'),
                new Digraph('a', 't')
            };


            var msg =
                @"According to an October 1998 report by the United States Bureau of Land Management, approximately 65% of Alaska is owned and managed by the U.S. federal government as public lands, including a multitude of national forests, national parks, and national wildlife refuges.[21] Of these, the Bureau of Land Management manages 87 million acres (35 million hectares), or 23.8% of the state. The Arctic National Wildlife Refuge is managed by the United States Fish and Wildlife Service. It is the world's largest wildlife refuge, comprising 16 million acres (6.5 million hectares).

Of the remaining land area, the state of Alaska owns 101 million acres (41 million hectares), its entitlement under the Alaska Statehood Act. A portion of that acreage is occasionally ceded to organized boroughs, under the statutory provisions pertaining to newly formed boroughs. Smaller portions are set aside for rural subdivisions and other homesteading-related opportunities. These are not very popular due to the often remote and roadless locations. The University of Alaska, as a land grant university, also owns substantial acreage which it manages independently.

Another 44 million acres (18 million hectares) are owned by 12 regional, and scores of local, Native corporations created under the Alaska Native Claims Settlement Act (ANCSA) of 1971. Regional Native corporation Doyon, Limited often promotes itself as the largest private landowner in Alaska in advertisements and other communications. Provisions of ANCSA allowing the corporations' land holdings to be sold on the open market starting in 1991 were repealed before they could take effect. Effectively, the corporations hold title (including subsurface title in many cases, a privilege denied to individual Alaskans) but cannot sell the land. Individual Native allotments can be and are sold on the open market, however.

Various private interests own the remaining land, totaling about one percent of the state. Alaska is, by a large margin, the state with the smallest percentage of private land ownership when Native corporation holdings are excluded.";

            Console.WriteLine($"{msg}\n");
            var key = "playfair example";
            var ecr = playfair.EncryptMessage(msg, key);
            
            Console.WriteLine($"{ecr}\n");
            
            
            var pa = new PlayfairAnalyser(new DigrathGenerator('X'));
            Console.WriteLine($"{pa.ReplaceMostCommonDigraphs(ecr, replacementDigraphs3)}\n");

            var dcr = playfair.DecryptMessage(ecr, key);
            Console.WriteLine(dcr);
            

            Console.ReadKey();
        }
    }
}