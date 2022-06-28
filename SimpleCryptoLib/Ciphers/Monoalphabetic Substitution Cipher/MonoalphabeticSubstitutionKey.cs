using System.Collections.Generic;
using SimpleCryptoLib.Ciphers.Common.Key_Management;

namespace SimpleCryptoLib.Ciphers.Monoalphabetic_Substitution_Cipher;

public class MonoalphabeticSubstitutionKey : CipherKeyBase
{
    /// <summary>
    /// Key (plain text), Value (substitution) key object.
    /// </summary>
    public IDictionary<char, char> SubstitutionMapping { get; set; }

    public MonoalphabeticSubstitutionKey(IDictionary<char, char> mapping)
    {
        SubstitutionMapping = mapping;
    }
}