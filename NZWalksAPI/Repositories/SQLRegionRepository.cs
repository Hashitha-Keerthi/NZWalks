using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domains;
using NZWalksAPI.Models.DTO;
using System.Runtime.InteropServices;

namespace NZWalksAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NzWalksDbContext dbContext;

        public SQLRegionRepository(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> Create(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> Delete(Guid id)
        {
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.id == id);
            if (regionDomain == null)
            {
                return null;
            }

            dbContext.Remove(regionDomain);
            await dbContext.SaveChangesAsync();
            return regionDomain;
        }

        public async Task<List<Region>> GetAllRegionAsync()
        {
          return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionById(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x=>x.id == id);
        }

        public async Task<Region?> Update(Guid id, Region region)
        {
            var exstingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);

            if (exstingRegion == null)
            {
                return null;
            }

            exstingRegion.Code = region.Code;
            exstingRegion.Name = region.Name;
            exstingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return exstingRegion;
        }
    }
}
