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

        internal void Remove(int keepId)
        {
            string sql = "DELETE FROM keeps WHERE id = @keepId";
            _db.Execute(sql, new { keepId });
        }
    }
}