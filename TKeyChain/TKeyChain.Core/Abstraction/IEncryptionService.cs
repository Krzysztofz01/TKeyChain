namespace TKeyChain.Core.Abstraction
{
    public interface IEncryptionService
    {
        string EncryptVault(string vaultSerialized, string key);
        string DecryptVault(string vaultCipher, string key);
    }
}
