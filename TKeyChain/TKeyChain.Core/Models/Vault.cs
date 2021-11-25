using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TKeyChain.Core.Exceptions;

namespace TKeyChain.Core.Models
{
    public class Vault
    {
        private readonly List<VaultEntity> _vaultEntities;

        public string GetPassword(string name)
        {
            var vaultEntity = _vaultEntities
                .SingleOrDefault(e => e.Name == name);

            if (vaultEntity is null)
                throw new VaultLogicException("No matching entity found.");

            return vaultEntity.Password;
        }

        public void InsertPassword(string name, string password)
        {
            if (_vaultEntities.Any(e => e.Name == name))
                throw new VaultLogicException("The name must be unique, and this name is already in use.");

            var vaultEntity = VaultEntity.Create(name, password);

            _vaultEntities.Add(vaultEntity);
        }

        public void RemovePassword(string name, string password)
        {
            var vaultEntity = _vaultEntities
                .SingleOrDefault(e => e.Name == name);

            if (vaultEntity is null)
                throw new VaultLogicException("No matching entity found.");

            _vaultEntities.Remove(vaultEntity);
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        private Vault() =>
            _vaultEntities = new List<VaultEntity>();

        public static Vault Create()
        {
            return new Vault();
        }
    }
}
