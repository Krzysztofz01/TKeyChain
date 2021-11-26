using System;
using TKeyChain.Core.Exceptions;
using TKeyChain.Core.Extensions;

namespace TKeyChain.Core.Models
{
    public class VaultEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }

        private VaultEntity() { }

        private static void Validate(string name, string password)
        {
            if (name.IsEmpty())
                throw new VaultLogicException("Invalid name format.");

            if (name.StartsWith("-") || name.StartsWith("--"))
                throw new VaultLogicException("Invalid name format.");

            if (password.IsEmpty())
                throw new VaultLogicException("Invalid password format.");
        }

        public static VaultEntity Create(string name, string password)
        {
            Validate(name, password);

            return new VaultEntity
            {
                Id = Guid.NewGuid(),
                Name = name,
                Password = password
            };
        }
    }
}
