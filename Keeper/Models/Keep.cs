

namespace Keeper.Models;

public class Keep : RepoItem<int>
{
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public int Views { get; set; }
    public Profile Creator { get; set; }
    public int kept { get; set; }
}

public class TrueKeep : Keep
{
    public int VaultKeepId { get; set; }
}
