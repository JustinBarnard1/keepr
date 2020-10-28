using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }
        internal void Create(VaultKeep newVc)
        {
            _repo.Create(newVc);
        }

        internal void Delete(string userId, int queryId)
        {

            var data = _repo.GetById(queryId);
            if (data == null) { throw new Exception("invalid Id"); }
            if (data.CreatorId != userId)
            {
                throw new Exception("This is not yours");
            }
            _repo.Delete(queryId);
        }
    }
}