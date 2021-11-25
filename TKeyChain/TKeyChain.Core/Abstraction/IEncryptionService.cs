namespace TKeyChain.Core.Abstraction
{
    public interface IEncryptionService
    {
        string EncryptVault(string vaultSerialized);
        string DecryptVault(string vaultCipher);
    }
}
