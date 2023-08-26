using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
    private readonly VaultsService _vaultsService;
    private readonly Auth0Provider _auth;
    private readonly VaultKeepsService _vaultKeepsService;

    public VaultsController(VaultsService vaultsService, Auth0Provider auth, VaultKeepsService vaultKeepsService)
    {
        _vaultsService = vaultsService;
        _auth = auth;
        _vaultKeepsService = vaultKeepsService;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
      try 
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        vaultData.CreatorId = userInfo.Id;
        Vault vault = _vaultsService.CreateVault(vaultData);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{vaultId}")]
    public ActionResult<Vault> GetVaultById(int vaultId)
    {
      try 
      {
        Vault vault = _vaultsService.GetVaultById(vaultId);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vaultData, int vaultId)
    {
      try 
      {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vaultData.Id = vaultId;
      Vault vault = _vaultsService.EditVault(vaultData, userInfo.Id);
      return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<string>> RemoveVault(int vaultId)
    {
      try 
      {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      _vaultsService.RemoveVault(vaultId, userInfo.Id);
      return Ok("Vault has been deleted.");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{vaultId}/keeps")]
    public ActionResult<List<TrueKeep>> GetKeepsByVault(int vaultId)
    {
      try 
      {
      List<TrueKeep> vaultKeeps = _vaultKeepsService.GetKeepsByVault(vaultId);
      return Ok(vaultKeeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
}
