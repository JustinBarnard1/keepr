using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Keep> GetAll()
        {
            string sql = @"
            SELECT
            k.*,
            p.*
            FROM keeps k
            JOIN profiles p ON k.creatorId = p.id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, splitOn: "id");
        }

        internal void AddView(int keepId)
        {
            string sql = @"
            UPDATE keeps
            SET
            views = views + 1
            WHERE
            id = @keepId;";
            _db.Execute(sql, new { keepId });
        }

        internal void AddKept(int keepId)
        {
            string sql = @"
            UPDATE keeps
            SET
            keeps = keeps + 1
            WHERE
            id = @keepId;";
            _db.Execute(sql, new { keepId });
        }

        internal IEnumerable<Keep> GetByVaultId(int id)
        {
            string sql = @"
            SELECT
            k.*,
            vk.*,
            p.*
            FROM
            keeps k
            JOIN vaultkeeps vk ON k.id = vk.keepId
            JOIN profiles p ON k.creatorId = p.id
            WHERE vk.vaultId = @id;";
            return _db.Query<Keep, VaultKeep, Profile, Keep>(sql, (keep, vaultkeep, profile) => { keep.Creator = profile; return keep; }, new { id }, splitOn: "id");
        }

        internal int Create(Keep newKeep)
        {
            string sql = @"
            INSERT INTO keeps
            (creatorId, name, description, img)
            VALUES
            (@CreatorId, @Name, @Description, @img);
            SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newKeep);
        }

        internal IEnumerable<Keep> GetByCreatorId(string queryId)
        {
            string sql = @"
            SELECT
            k.*,
            p.*
            FROM keeps k
            JOIN profiles p ON k.creatorId = p.id
            WHERE creatorId = @queryId;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { queryId }, splitOn: "id");
        }

        internal Keep GetById(int keepId)
        {
            string sql = @"
            SELECT
            k.*,
            p.*
            FROM keeps k
            JOIN profiles p ON k.creatorId = p.id
            WHERE k.id = @keepId;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { keepId }, splitOn: "id").FirstOrDefault();
        }

        internal Keep Edit(Keep editKeep)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            img = @Img
            WHERE id = @Id;";
            _db.Execute(sql, editKeep);
            return editKeep;
        }

        internal void Remove(int keepId)
        {
            string sql = "DELETE FROM keeps WHERE id = @keepId";
            _db.Execute(sql, new { keepId });
        }
    }
}