

namespace Keeper.Models;

public class VaultKeep : RepoItem<int>
{
    public string CreatorId { get; set; }
    public int VaultId { get; set; }
    public int keepId { get; set; }
}
