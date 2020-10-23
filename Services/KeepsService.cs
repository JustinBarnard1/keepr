using System;
using System.Collections.Generic;
using System.Linq;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Keep> GetAll()
        {
            return _repo.GetAll();
        }

        internal Keep GetById(string profileId, int keepId)
        {
            _repo.AddView(keepId);
            Keep original = _repo.GetById(keepId);
            if (original == null) { throw new Exception("Invalid Id"); }
            return original;
        }

        internal Keep Create(Keep newKeep)
        {
            newKeep.Id = _repo.Create(newKeep);
            return newKeep;
        }

        internal object Delete(int keepId, string userId)
        {
            Keep original = _repo.GetById(keepId);
            if (original == null) { throw new Exception("Invalid Id"); }
            if (original.CreatorId != userId) { throw new Exception("Access Denied. This is not yours"); }
            _repo.Remove(keepId);
            return "Succesfully Delorted";
        }
    }
}