using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.Services;

public class VaultsService
{

  private readonly VaultsRepository _vaultsRepository;

    public VaultsService(VaultsRepository vaultsRepository)
    {
        _vaultsRepository = vaultsRepository;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        int vaultId = _vaultsRepository.CreateVault(vaultData);
        Vault vault = GetVaultById(vaultId, vaultData.CreatorId);
        return vault;

    }

    internal Vault EditVault(Vault vaultData, string userId)
    {
        Vault originalVault = GetVaultById(vaultData.Id, userId);
        if( originalVault.CreatorId != userId)
        {
          throw new Exception("You cannot edit vaults you do not own");
        }
        originalVault.Name = vaultData.Name ?? originalVault.Name;
        originalVault.Description = vaultData.Description ?? originalVault.Description;
        originalVault.Img = vaultData.Img ?? originalVault.Img;
        originalVault.isPrivate = vaultData.isPrivate ?? originalVault.isPrivate;

        _vaultsRepository.EditVault(originalVault);
        return originalVault;
    }

    internal List<Vault> GetAccountVaults(string userId)
    {
        List<Vault> vaults = _vaultsRepository.GetAccountVaults(userId);
        return vaults;
    }

    internal List<Vault> GetProfileVaults(string profileId, string userId)
    {
        List<Vault> vaults = _vaultsRepository.GetProfileVaults(profileId);
        vaults = vaults.FindAll(v => v.isPrivate == false || v.isPrivate == true && v.CreatorId == userId);

        return vaults;
    }

    internal Vault GetVaultById(int vaultId, string userId = null)
    {
      Vault vault = _vaultsRepository.GetVaultById(vaultId);
      if(vault == null)
      {
        throw new Exception($"Invalid Id: {vaultId}");
      }
      if(vault.isPrivate == true && vault.CreatorId != userId)
      {
        throw new Exception("This is a private vault");
      }
      return vault;
    }

    internal void RemoveVault(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId, userId);
        if(vault.CreatorId != userId)
        {
          throw new Exception("You cannot delete someone else's Vault.");
        }
        _vaultsRepository.RemoveVault(vaultId);
    }
}
