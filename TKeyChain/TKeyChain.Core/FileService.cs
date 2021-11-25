using System;
using System.IO;
using TKeyChain.Core.Abstraction;
using TKeyChain.Core.Exceptions;
using TKeyChain.Core.Extensions;

namespace TKeyChain.Core
{
    public class FileService : IFileService
    {
        private readonly string _directoryName = "TKeyChain";
        private readonly string _vaultName = "tkcVault";

        public void AppendVaultFile(string cipher)
        {
            if (!File.Exists(GetVaultPath()))
                throw new VaultIOException("The vault is not initialized.");

            using var sw = File.CreateText(GetVaultPath());

            sw.WriteLine(cipher);
        }

        public void BackupVaultFile(string path)
        {
            if (!File.Exists(GetVaultPath()))
                throw new VaultIOException("The vault is not initialized.");

            string vaultContent = File.ReadAllText(GetVaultPath());

            if (vaultContent.IsEmpty())
                throw new IOException("Vault content unavailable.");

            if (File.Exists(path))
                throw new VaultIOException("The given path is not available.");

            using var sw = File.CreateText(path);

            sw.WriteLine(vaultContent);
        }

        public string GetVaultFileContent()
        {
            if (!File.Exists(GetVaultPath()))
                throw new VaultIOException("The vault is not initialized.");

            string vaultContent = File.ReadAllText(GetVaultPath());

            if (vaultContent.IsEmpty())
                throw new IOException("Vault content unavailable.");

            return vaultContent;
        }

        public void InitializeVaultFile(string cipher)
        {
            if (File.Exists(GetVaultPath()))
                throw new VaultIOException("The vault is already created.");

            using var sw = File.CreateText(GetVaultPath());
            
            sw.WriteLine(cipher);
        }

        private string GetVaultPath()
        {
            string vaultDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _directoryName);

            string vaultFilePath = Path.Combine(vaultDirectoryPath, _vaultName);

            return vaultFilePath;
        }
    }
}
