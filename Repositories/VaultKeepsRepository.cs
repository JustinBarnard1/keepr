using System;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;
        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal int Create(VaultKeep newVc)
        {
            string sql = @"
            INSERT INTO vaultkeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@CreatorId, @VaultId, @KeepId);
            SELECT LAST_INSERT_ID(); ";
            return _db.ExecuteScalar<int>(sql, newVc);
        }

        internal VaultKeep Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
            var deleted = _db.ExecuteScalar<VaultKeep>(sql, new { id });
            return deleted;
        }

        internal VaultKeep GetById(int id)
        {
            string sql = @"
            SELECT
            *
            FROM vaultkeeps
            WHERE id = @id;";
            var found = _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
            return found;
        }
    }
}