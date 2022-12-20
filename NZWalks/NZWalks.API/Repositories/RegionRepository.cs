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
        public IEnumerable<Region> GetAll()
        {
            //get all the regions
            return nZWalksDbContext.Regions.ToList();            
        }
    }
}
