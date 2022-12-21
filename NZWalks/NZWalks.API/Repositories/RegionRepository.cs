using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        //fetching the database from injecting in services,using creating constroctor
        public RegionRepository(NZWalksDbContext nZWalksDbContext) 
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        /*make below function to asynchronous
         * public IEnumerable<Region> GetAll()
         {
             //get all the regions
             return nZWalksDbContext.Regions.ToList();            
         }*/
        //asynchronous 

        //add region
        public async Task<Region>AddAsync(Region region) 
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region= await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(region==null)
            {
                return null;
            }
            //delete the region
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;

        }

        public async Task<IEnumerable<Region>> GetAllAsync()//change to async
        {
            //get all the regions
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
           return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x=>x.Id== id);
           
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingregion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingregion == null)
            {
                return null;
            }
            //update the region
           existingregion.Code=region.Code;
            existingregion.Name=region.Name;
            existingregion.Area=region.Area;
            existingregion.Lat=region.Lat;
            existingregion.Long=region.Long;
            existingregion.Population=region.Population;
            await nZWalksDbContext.SaveChangesAsync();
            return existingregion;

        }
    }
}
