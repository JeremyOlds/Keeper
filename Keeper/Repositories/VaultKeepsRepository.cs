using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal int CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO vaultKeeps(vaultId, keepId, creatorId)
        VALUES(@VaultId, @KeepId, @CreatorId);
        SELECT LAST_INSERT_ID()
        ;";
        int vaultKeepId = _db.ExecuteScalar<int>(sql, vaultKeepData);
        return vaultKeepId;
    }

    internal List<TrueKeep> GetKeepsByVault(int vaultId)
    {
        string sql = @"
        SELECT
        vk.*,
        keeps.*,
        acc.*
        FROM vaultKeeps vk
        JOIN keeps ON vk.keepId = keeps.Id
        JOIN accounts acc ON acc.id = keeps.creatorId
        WHERE vk.vaultId = @vaultId
        ;";

        List<TrueKeep> vaultKeeps = _db.Query<VaultKeep, TrueKeep , Profile, TrueKeep>(sql,
        (vaultKeep, trueKeep, profile)=>
        {
          trueKeep.VaultKeepId = vaultKeep.Id;
          trueKeep.Creator = profile;
          return trueKeep;
        },
        new {vaultId}).ToList();
        return vaultKeeps;
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        string sql = @"
        SELECT 
        *
        FROM vaultKeeps
        WHERE id = @vaultKeepId
        ;";

        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new {vaultKeepId}).FirstOrDefault();
        return vaultKeep;
    }

    internal void RemoveVaultKeep(int vaultKeepId)
    {
        string sql = @"
        DELETE FROM vaultKeeps
        WHERE id = @vaultKeepId
        ;";
        _db.Execute(sql, new {vaultKeepId});
    }
    internal List<VaultKeep> GetVaultKeeps()
    {
        string sql = @"
        SELECT
        *
        FROM vaultKeeps
        ;";

        List<VaultKeep> vaultkeeps = _db.Query<VaultKeep>(sql).ToList();
        return vaultkeeps;
    }
}
