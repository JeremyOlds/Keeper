using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _vaultKeepsRepository;
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService, KeepsService keepsService)
    {
        _vaultKeepsRepository = vaultKeepsRepository;
        _vaultsService = vaultsService;
        _keepsService = keepsService;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
      Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
      if(vault.CreatorId != vaultKeepData.CreatorId)
      {
        throw new Exception("You cannot create a VaultKeep for someone else's vault.");
      }
      List<VaultKeep> vaultKeeps = _vaultKeepsRepository.GetVaultKeeps();
      vaultKeeps.ForEach(vk => 
      {
        if(vk.keepId == vaultKeepData.keepId && vk.VaultId == vaultKeepData.VaultId)
        {
          throw new Exception("you cannot have multiple copies of keeps in your vault");
        }
      });
        int vaultKeepId = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);

      VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
        return vaultKeep;
    }

    internal List<TrueKeep> GetKeepsByVault(int vaultId, string userId)
    {
      Vault vault = _vaultsService.GetVaultById(vaultId, userId);
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
