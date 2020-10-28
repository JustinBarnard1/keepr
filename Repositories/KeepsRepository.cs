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

        internal IEnumerable<ViewModelKeep> GetAll()
        {
            string sql = @"
            SELECT
            k.*,
            p.*
            FROM keeps k
            JOIN profiles p ON k.creatorId = p.id;";
            return _db.Query<ViewModelKeep, Profile, ViewModelKeep>(sql, (keep, profile) =>
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

        internal IEnumerable<ViewModelKeep> GetByVaultId(int id)
        {
            string sql = @"
            SELECT
            k.*,
            vk.id AS vaultKeepId,
            p.*
            FROM
            keeps k
            JOIN vaultkeeps vk ON k.id = vk.keepId
            JOIN profiles p ON k.creatorId = p.id
            WHERE vk.vaultId = @id;";
            return _db.Query<ViewModelKeep, Profile, ViewModelKeep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { id }, splitOn: "id");
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

        internal IEnumerable<ViewModelKeep> GetByCreatorId(string queryId)
        {
            string sql = @"
            SELECT
            k.*,
            p.*
            FROM keeps k
            JOIN profiles p ON k.creatorId = p.id
            WHERE creatorId = @queryId;";
            return _db.Query<ViewModelKeep, Profile, ViewModelKeep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { queryId }, splitOn: "id");
        }

        internal ViewModelKeep GetById(int keepId)
        {
            string sql = @"
            SELECT
            k.*,
            p.*
            FROM keeps k
            JOIN profiles p ON k.creatorId = p.id
            WHERE k.id = @keepId;";
            return _db.Query<ViewModelKeep, Profile, ViewModelKeep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { keepId }, splitOn: "id").FirstOrDefault();
        }

        internal ViewModelKeep Edit(ViewModelKeep editKeep)
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