using System;
using System.Collections.Generic;
using System.Linq;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Vault> GetVaultsById(string userId, string queryId)
        {
            IEnumerable<Vault> vaults = _repo.GetById(queryId);
            return vaults.ToList().FindAll(v => v.CreatorId == userId || v.IsPrivate == false);
        }

        internal Vault Create(Vault newVault)
        {
            newVault.Id = _repo.Create(newVault);
            return newVault;
        }

        internal object Delete(string queryId, string userId)
        {
            Vault original = _repo.GetVaultById(queryId);
            if (original == null) { throw new Exception("Invalid Id"); }
            if (original.CreatorId != userId) { throw new Exception("Access Denied, This is not yours."); }
            _repo.Remove(queryId);
            return "Successfully Delorted";
        }

        internal Vault GetById(string userId, string queryId)
        {
            return _repo.GetVaultById(queryId);
        }
    }
}