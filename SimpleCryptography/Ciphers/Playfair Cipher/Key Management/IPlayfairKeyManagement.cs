using SimpleCryptography.Ciphers.Generic.Key_Management;

namespace SimpleCryptography.Ciphers.Playfair_Cipher.Key_Management
{
    /// <summary>
    /// Playfair key generation and validation.
    /// </summary>
    public interface IPlayfairKeyManagement : ICipherKeyManagement<PlayfairKey>
    {
    }
}