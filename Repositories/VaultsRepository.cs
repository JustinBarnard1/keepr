using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;
        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Vault> GetById(string queryId)
        {
            string sql = @"
            SELECT
            v.*,
            p.*
            FROM vaults v
            JOIN profiles p ON v.creatorId = p.id
            WHERE creatorId = @queryId;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { queryId }, splitOn: "id");
        }

        internal int Create(Vault newVault)
        {
            string sql = @"
            INSERT INTO vaults
            (creatorId, name, description, isPrivate)
            VALUES
            (@CreatorId, @Name, @Description, @IsPrivate);
            SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newVault);
        }

        internal void Remove(string queryId)
        {
            string sql = "DELETE FROM vaults WHERE id = @queryId";
            _db.Execute(sql, new { queryId });
        }

        internal Vault GetVaultById(string queryId)
        {
            string sql = @"
            SELECT
            v.*,
            p.*
            FROM vaults v
            JOIN profiles p ON v.creatorId = p.id
            WHERE v.id = @queryId;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { queryId }, splitOn: "id").FirstOrDefault();
        }
    }
}