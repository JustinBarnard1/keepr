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

        internal IEnumerable<Keep> GetAllByVaultId(int id)
        {
            return _repo.GetByVaultId(id);
        }

        internal object Delete(int keepId, string userId)
        {
            Keep original = _repo.GetById(keepId);
            if (original == null) { throw new Exception("Invalid Id"); }
            if (original.CreatorId != userId) { throw new Exception("Access Denied. This is not yours"); }
            _repo.Remove(keepId);
            return "Succesfully Delorted";
        }

        internal IEnumerable<Keep> GetAllByCreatorId(string queryId, string userId)
        {
            return _repo.GetByCreatorId(queryId);
        }

        internal Keep Edit(Keep editKeep, string id)
        {
            Keep original = _repo.GetById(editKeep.Id);
            if (original == null) { throw new Exception("Invalid Id"); }
            if (original.CreatorId != id) { throw new Exception("Access Denied, this is not yours"); }
            editKeep.Name = editKeep.Name == null ? original.Name : editKeep.Name;
            editKeep.Description = editKeep.Description == null ? original.Description : editKeep.Description;
            editKeep.Img = editKeep.Img == null ? original.Img : editKeep.Img;
            return _repo.Edit(editKeep);
        }
    }
}