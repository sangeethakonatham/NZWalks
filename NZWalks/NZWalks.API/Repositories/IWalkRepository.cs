using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        //creating Method for repository
        Task<IEnumerable<Walk>> GetAllAsync();
        //creating repository method for Getwalk
        Task<Walk> GetAsync(Guid id);
        //creating Repository method for AddWalk
        Task<Walk> AddAsync(Walk walk);
        //creating Repository method for UpdateWalk
        Task<Walk> UpdateAsync(Guid id,Walk walk);
        //creating Repository method for DeleteWalk
        Task<Walk> DeleteAsync(Guid id);



    }
}
