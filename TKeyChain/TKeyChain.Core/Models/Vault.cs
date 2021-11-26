using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using TKeyChain.Core.Exceptions;

namespace TKeyChain.Core.Models
{
    public class Vault
    {
        public List<VaultEntity> VaultEntities { get; init; }

        public string GetPassword(string name)
        {
            var vaultEntity = VaultEntities
                .SingleOrDefault(e => e.Name == name);

            if (vaultEntity is null)
                throw new VaultLogicException("No matching entity found.");

            return vaultEntity.Password;
        }

        public IEnumerable<string> GetAllPasswordNames()
        {
            return VaultEntities
                .Select(v => v.Name);
        }

        public void InsertPassword(string name, string password)
        {
            if (VaultEntities.Any(e => e.Name == name))
                throw new VaultLogicException("The name must be unique, and this name is already in use.");

            var vaultEntity = VaultEntity.Create(name, password);

            VaultEntities.Add(vaultEntity);
        }

        public void RemovePassword(string name)
        {
            var vaultEntity = VaultEntities
                .SingleOrDefault(e => e.Name == name);

            if (vaultEntity is null)
                throw new VaultLogicException("No matching entity found.");

            VaultEntities.Remove(vaultEntity);
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this, typeof(Vault), new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.Never
            });
        }

        public Vault() =>
            VaultEntities = new List<VaultEntity>();

        public static Vault Create()
        {
            return new Vault();
        }

        public static Vault Deserialize(string serializedVault)
        {
            return JsonSerializer.Deserialize<Vault>(serializedVault);
        }
    }
}
