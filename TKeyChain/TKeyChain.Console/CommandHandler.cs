using System;
using System.Linq;
using TKeyChain.Core;
using TKeyChain.Core.Abstraction;
using TKeyChain.Core.Models;

namespace TKeyChain.Cli
{
    public class CommandHandler
    {
        private readonly string _secretEnterPrompt = "Enter the master password: ";

        private readonly IFileService _fileService;
        private readonly IEncryptionService _encryptionService;
        private readonly IClipboardService _clipboardService;

        public CommandHandler()
        {
            _fileService = new FileService();
            _encryptionService = new EncryptionService();

            // TODO: Implement the clipboard service
            _clipboardService = null;
        }

        public void Get(string[] args)
        {
            bool copyToClipboard = true;
            bool printToTerminal = false;
            bool printAllPasswordNames = false;

            if (args.Any(a => a.ToLower() == "-n" || a.ToLower() == "--no-clipboard")) copyToClipboard = false;
            if (args.Any(a => a.ToLower() == "-p" || a.ToLower() == "--print")) printToTerminal = true;
            if (args.Any(a => a.ToLower() == "-l" || a.ToLower() == "--list")) printAllPasswordNames = true;
            
            string passwordName = args.Last();

            string masterPassword = ConsoleUtility.ReadSecret(_secretEnterPrompt);

            string cipher = _fileService.GetVaultFileContent();

            string serializedVault = _encryptionService.DecryptVault(cipher, masterPassword);

            var vault = Vault.Deserialize(serializedVault);

            if (printAllPasswordNames)
            {
                var names = vault.GetAllPasswordNames();

                if (!names.Any())
                {
                    Console.WriteLine("No passwords stored in the vault.");
                    return;
                }

                foreach (var name in names) Console.WriteLine(name);

                return;
            }

            string password = vault.GetPassword(passwordName);

            if (copyToClipboard)
            {
                _clipboardService.CopyToClipboard(password);
                Console.WriteLine("Password copied to clipboard...");
            }

            if (printToTerminal)
            {
                Console.WriteLine($"Password for {passwordName}:");
                Console.WriteLine(password);
            }
        }

        public void Insert(string[] args)
        {
            if (args.Length != 2)
                throw new ArgumentException("Invalid argumenets.");

            if (args.Last().StartsWith("-"))
                throw new ArgumentException("Invalid argumenets.");

            string passwordName = args.Last();

            string masterPassword = ConsoleUtility.ReadSecret(_secretEnterPrompt);

            string cipher = _fileService.GetVaultFileContent();

            string serializedVault = _encryptionService.DecryptVault(cipher, masterPassword);

            var vault = Vault.Deserialize(serializedVault);

            string password = ConsoleUtility.ReadSecret("Enter the password you want to store: ");

            string passwordRepeat = ConsoleUtility.ReadSecret("Repeat the password you want to store: ");

            if (password != passwordRepeat)
                throw new ArgumentException("The given passwords are not matching.");

            vault.InsertPassword(passwordName, password);

            string updatedSerializedVault = vault.Serialize();

            string updatedCipher = _encryptionService.EncryptVault(updatedSerializedVault, masterPassword);

            _fileService.AppendVaultFile(updatedCipher);

            Console.WriteLine("Password successful stored in the vault.");
        }

        public void Remove(string[] args)
        {
            if (args.Length != 2)
                throw new ArgumentException("Invalid argumenets.");

            if (args.Last().StartsWith("-"))
                throw new ArgumentException("Invalid argumenets.");

            string passwordName = args.Last();

            string masterPassword = ConsoleUtility.ReadSecret(_secretEnterPrompt);

            string cipher = _fileService.GetVaultFileContent();

            string serializedVault = _encryptionService.DecryptVault(cipher, masterPassword);

            var vault = Vault.Deserialize(serializedVault);

            vault.RemovePassword(passwordName);

            string updatedSerializedVault = vault.Serialize();

            string updatedCipher = _encryptionService.EncryptVault(updatedSerializedVault, masterPassword);

            _fileService.AppendVaultFile(updatedCipher);

            Console.WriteLine("Password sucessful removed from the vault.");
        }

        public void Initialize(string[] args)
        {
            string masterPassword = ConsoleUtility.ReadSecret(_secretEnterPrompt);

            var serializedVault = Vault.Create().Serialize();

            var cipher = _encryptionService.EncryptVault(serializedVault, masterPassword);

            _fileService.InitializeVaultFile(cipher);

            Console.WriteLine("Vault initialized successful.");
        }
    }
}
