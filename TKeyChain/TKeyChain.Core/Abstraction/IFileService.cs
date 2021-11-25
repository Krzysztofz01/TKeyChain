namespace TKeyChain.Core.Abstraction
{
    public interface IFileService
    {
        public string GetVaultFileContent();
        public string InitializeVaultFile();
        public void AppendVaultFile();
        public void BackupVaultFile();
    }
}
