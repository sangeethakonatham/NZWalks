using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        //IEnumerable<Region> GetAll();
       // make below to asynchronous
        Task<IEnumerable<Region>> GetAllAsync();//change to async
        Task<Region>GetAsync(Guid id);//creating single region Repository
        Task<Region>AddAsync(Region region);    //adding region
        Task<Region> DeleteAsync(Guid id); //deleteing region

        Task<Region> UpdateAsync(Guid id,Region region);//update region

    }
}
