using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _vaultKeepsRepository;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository)
    {
        _vaultKeepsRepository = vaultKeepsRepository;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        int vaultKeepId = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);
      VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
        return vaultKeep;
    }

    internal List<TrueKeep> GetKeepsByVault(int vaultId)
    {
      List<TrueKeep> vaultKeeps = _vaultKeepsRepository.GetKeepsByVault(vaultId);
      return vaultKeeps;
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
      VaultKeep vaultKeep = _vaultKeepsRepository.GetVaultKeepById(vaultKeepId);
      if(vaultKeep == null)
      {
        throw new Exception($"Invalid Id: {vaultKeepId}");
      }
      return vaultKeep;
    }

    internal void RemoveVaultKeep(int vaultKeepId, string userId)
    {
        VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
        if(vaultKeep.CreatorId != userId)
        {
          throw new Exception("You cannot delete someone else's VaultKeep");
        }
        _vaultKeepsRepository.RemoveVaultKeep(vaultKeepId);
    }
}
