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
        Vault vault = GetVaultById(vaultId);
        return vault;

    }

    internal Vault EditVault(Vault vaultData, string userId)
    {
        Vault originalVault = GetVaultById(vaultData.Id);
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

    internal Vault GetVaultById(int vaultId)
    {
      Vault vault = _vaultsRepository.GetVaultById(vaultId);
      if(vault == null)
      {
        throw new Exception($"Invalid Id: {vaultId}");
      }
      return vault;
    }

    internal void RemoveVault(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId);
        if(vault.CreatorId != userId)
        {
          throw new Exception("You cannot delete someone else's Vault.");
        }
        _vaultsRepository.RemoveVault(vaultId);
    }
}
