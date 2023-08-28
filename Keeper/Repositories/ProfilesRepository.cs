using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.Repositories;

public class ProfilesRepository
{
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Profile GetProfile(string profileId)
    {
      string sql = @"
      SELECT
      *
      FROM accounts acc
      WHERE acc.id = @profileId
      LIMIT 1
      ;";

      Profile profile = _db.Query<Profile>(sql, new {profileId}).FirstOrDefault();
      return profile;
    }
}
