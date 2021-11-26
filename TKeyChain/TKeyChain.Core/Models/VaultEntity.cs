using System;
using TKeyChain.Core.Exceptions;
using TKeyChain.Core.Extensions;

namespace TKeyChain.Core.Models
{
    public class VaultEntity
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Password { get; init; }

        [Obsolete("The parameterless constructor should be used only by the serializer.")]
        public VaultEntity() { }

        private VaultEntity(string name, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Password = password;
        }

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

            return new VaultEntity(name, password);
        }
    }
}
