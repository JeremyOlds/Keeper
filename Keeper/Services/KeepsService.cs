

namespace Keeper.Services;

public class KeepsService
{
    private readonly KeepsRepository _keepsRepository;

    public KeepsService(KeepsRepository keepsRepository)
    {
        _keepsRepository = keepsRepository;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        int keepId = _keepsRepository.CreateKeep(keepData);
        Keep keep = GetKeepById(keepId);
        return keep;
    }

    internal Keep EditKeep(Keep keepData, string userId)
    {
        Keep originalKeep = GetKeepById(keepData.Id);
        if(originalKeep.CreatorId != userId)
        {
          throw new Exception("You cannot edit another persons Keep");
        }
        originalKeep.Name = keepData.Name ?? originalKeep.Name;
        originalKeep.Description = keepData.Description ?? originalKeep.Description;
        originalKeep.Img = keepData.Img ?? originalKeep.Img;

        _keepsRepository.EditKeep(originalKeep);
        return originalKeep;
    }

    internal Keep GetKeepById(int keepId)
    {
      Keep keep = _keepsRepository.GetKeepById(keepId);
      if(keep == null)
      {
        throw new Exception($"Invalid Id: {keepId}");
      }
      return keep;

    }

    internal List<Keep> GetKeeps()
    {
      List<Keep> keeps = _keepsRepository.GetKeeps();
      return keeps;
    }

    internal void RemoveKeep(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId);
        if(keep.CreatorId != userId)
        {
          throw new Exception("You cannot delete other people's Keeps.");
        }
        _keepsRepository.removeKeep(keepId);
    }
}
