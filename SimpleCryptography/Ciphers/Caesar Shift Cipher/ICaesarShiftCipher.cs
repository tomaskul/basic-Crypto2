using SimpleCryptography.Ciphers.Generic;

namespace SimpleCryptography.Ciphers.Caesar_Shift_Cipher
{
    /// <summary>
    /// Caesar shift cipher.
    /// </summary>
    public interface ICaesarShiftCipher : IGenericCipher<CaesarCipherKey>
    {
    }
}