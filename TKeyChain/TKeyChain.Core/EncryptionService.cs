using System;
using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;
using TKeyChain.Core.Abstraction;

namespace TKeyChain.Core
{
    public class EncryptionService : IEncryptionService
    {
        private const int _saltSize = 8;
        private const int _bytesDeriveIterations = 1000;

        public string DecryptVault(string vaultCipher, string key)
        {
            Span<byte> encryptedData = Convert.FromBase64String(vaultCipher).AsSpan();

            using var aesGcm = CreateAesGcm(key);

            int nonceSize = BinaryPrimitives.ReadInt32LittleEndian(encryptedData.Slice(0, 4));
            int tagSize = BinaryPrimitives.ReadInt32LittleEndian(encryptedData.Slice(4 + nonceSize, 4));
            int cipherSize = encryptedData.Length - 4 - nonceSize - 4 - tagSize;

            var nonce = encryptedData.Slice(4, nonceSize);
            var tag = encryptedData.Slice(4 + nonceSize + 4, tagSize);
            var cipherBytes = encryptedData.Slice(4 + nonceSize + 4 + tagSize, cipherSize);

            Span<byte> plainBytes = cipherSize < 1024
                ? stackalloc byte[cipherSize]
                : new byte[cipherSize];

            aesGcm.Decrypt(nonce, cipherBytes, tag, plainBytes);

            return Encoding.UTF8.GetString(plainBytes);
        }

        public string EncryptVault(string vaultSerialized, string key)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(vaultSerialized);

            using var aesGcm = CreateAesGcm(key);

            int nonceSize = AesGcm.NonceByteSizes.MaxSize;
            int tagSize = AesGcm.TagByteSizes.MaxSize;
            int cipherSize = plainBytes.Length;

            int encryptedDataLength = 4 + nonceSize + 4 + tagSize + cipherSize;
            
            Span<byte> encryptedData = encryptedDataLength < 1024
                ? stackalloc byte[encryptedDataLength]
                : new byte[encryptedDataLength].AsSpan();

            BinaryPrimitives.WriteInt32LittleEndian(encryptedData.Slice(0, 4), nonceSize);
            BinaryPrimitives.WriteInt32LittleEndian(encryptedData.Slice(4 + nonceSize, 4), tagSize);

            var nonce = encryptedData.Slice(4, nonceSize);
            var tag = encryptedData.Slice(4 + nonceSize + 4, tagSize);
            var cipherBytes = encryptedData.Slice(4 + nonceSize + 4 + tagSize, cipherSize);

            RandomNumberGenerator.Fill(nonce);

            aesGcm.Encrypt(nonce, plainBytes.AsSpan(), cipherBytes, tag);

            return Convert.ToBase64String(encryptedData);
        }

        private AesGcm CreateAesGcm(string key)
        {
            byte[] salt = new byte[_saltSize];

            byte[] keyBytes = new Rfc2898DeriveBytes(key, salt, _bytesDeriveIterations).GetBytes(16);

            return new AesGcm(keyBytes);
        }
    }
}
