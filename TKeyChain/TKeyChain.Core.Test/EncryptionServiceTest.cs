using System.Security.Cryptography;
using TKeyChain.Core.Abstraction;
using Xunit;

namespace TKeyChain.Core.Test
{
    public class EncryptionServiceTest
    {
        [Fact]
        public void ServiceShouldEncryptValidString()
        {
            // Arrange
            string plainText = "Hello world!";
            
            string correctKey = "My key!";

            IEncryptionService encryptionService = new EncryptionService();

            // Operation
            string cipher = encryptionService.EncryptVault(plainText, correctKey);

            // Assertion
            Assert.NotNull(cipher);
        }

        [Fact]
        public void ServiceShouldDecryptWithValidKey()
        {
            // Arrange
            string plainText = "Hello world!";

            string correctKey = "My key!";

            IEncryptionService encryptionService = new EncryptionService();

            // Operation
            string cipher = encryptionService.EncryptVault(plainText, correctKey);

            IEncryptionService encryptionServiceForDecryption = new EncryptionService();
            string decryptedText = encryptionServiceForDecryption.DecryptVault(cipher, correctKey);

            // Assertion
            Assert.Equal(plainText, decryptedText);
        }

        [Fact]
        public void ServiceShouldNotDecryptWithInvalidKey()
        {
            // Arrange
            string plainText = "Hello world!";

            string correctKey = "My key!";
            string wrongKey = "Not my key!";

            IEncryptionService encryptionService = new EncryptionService();

            // Operation
            string cipher = encryptionService.EncryptVault(plainText, correctKey);

            IEncryptionService encryptionServiceForDecryption = new EncryptionService();

            // Assertion
            Assert.Throws<CryptographicException>(() => encryptionServiceForDecryption.DecryptVault(cipher, wrongKey));
        }
    }
}
