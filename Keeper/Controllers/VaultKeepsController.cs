

namespace Keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly Auth0Provider _auth;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
    {
        _vaultKeepsService = vaultKeepsService;
        _auth = auth;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
      try 
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        vaultKeepData.CreatorId = userInfo.Id;
        VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData);
        return Ok(vaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{vaultKeepId}")]
    [Authorize]
    public async Task<ActionResult<string>> RemoveVaultKeep(int vaultKeepId)
    {
      try 
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        _vaultKeepsService.RemoveVaultKeep(vaultKeepId, userInfo.Id);
        return Ok("VaultKeep has been deleted.");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
}
