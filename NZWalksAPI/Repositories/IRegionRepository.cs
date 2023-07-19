using NZWalksAPI.Models.Domains;

namespace NZWalksAPI.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionAsync();
        Task<Region?> GetRegionById(Guid id);

        Task<Region> Create(Region region);
        Task<Region?> Update(Guid id, Region region);

        Task<Region?> Delete(Guid id);
    }
}
