namespace TKeyChain.Core.Abstraction
{
    public interface IFileService
    {
        public string GetVaultFileContent();
        public void InitializeVaultFile(string cipher);
        public void AppendVaultFile(string cipher);
        public void BackupVaultFile(string path);
    }
}
