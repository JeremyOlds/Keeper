

namespace Keeper.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal int CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO keeps(name, description, img, views, creatorId)
        VALUES(@Name, @Description, @Img, @Views, @creatorId);
        SELECT LAST_INSERT_ID()
        ;";
        int keepId = _db.ExecuteScalar<int>(sql, keepData);
        return keepId;
    }

    internal void EditKeep(Keep originalKeep)
    {
        string sql = @"
        UPDATE keeps
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views
        WHERE id = @id
        ;";
        _db.Execute(sql, originalKeep);
        
    }



    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT
        keeps.*,
        COUNT(vk.id) AS kept,
        acc.*
        FROM keeps
        LEFT JOIN vaultKeeps vk ON vk.keepId = keeps.id
        JOIN accounts acc ON acc.id = keeps.creatorId
        WHERE keeps.id = @keepId
        ;";

        Keep keep = _db.Query<Keep, Profile, Keep>(
          sql,
          (keep, profile)=>
          {
            keep.Creator = profile;
            return keep;
          }, new {keepId}
        ).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        string sql = @"
        SELECT
        keeps.*,
        acc.*
        FROM keeps
        JOIN accounts acc ON acc.id = keeps.creatorId
        ;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(
          sql,
          (keep, profile)=>
          {
            keep.Creator = profile;
            return keep;
          }
        ).ToList();
        return keeps;
    }

    internal List<Keep> GetProfileKeeps(string profileId)
    {
        string sql = @"
        SELECT
        keeps.*,
        acc.*
        FROM keeps
        JOIN accounts acc ON acc.id = keeps.creatorId
        WHERE keeps.creatorId = @profileId
        ;";
        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(
          sql,
          (keep, profile)=>
          {
            keep.Creator = profile;
            return keep;
          }
          ,new {profileId}).ToList();
        return keeps;
    }

    internal void removeKeep(int keepId)
    {
        string sql = @"
        DELETE FROM keeps
        WHERE id = @keepId
        LIMIT 1
        ;";
        _db.Execute(sql,new {keepId});
    }
}
