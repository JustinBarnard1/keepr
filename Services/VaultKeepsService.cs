using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsRepository _vRepo;
        public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vRepo)
        {
            _repo = repo;
            _vRepo = vRepo;
        }
        internal VaultKeep Create(string userId, VaultKeep newVc)
        {
            var data = _vRepo.GetVaultById(newVc.VaultId.ToString());
            if (data.CreatorId != userId)
            {
                throw new Exception("This is not your Vault");
            }
            newVc.Id = _repo.Create(newVc);
            return newVc;
        }

        internal VaultKeep Delete(string userId, int queryId)
        {

            var data = _repo.GetById(queryId);
            if (data == null) { throw new Exception("invalid Id"); }
            if (data.CreatorId != userId)
            {
                throw new Exception("This is not yours");
            }
            var deleted = _repo.Delete(queryId);
            return deleted;
        }
    }
}