using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        //injecting service inside the constructor,injecting irepository interface

        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()//change to async
        {
            //var regions = regionRepository.GetAll();changing method to async method
            var regions = await regionRepository.GetAllAsync();
            //static list
            /*var regions = new List<Region>()
            { 
                new Region
                {
                    Id=Guid.NewGuid(),  
                    Name="Wellington",
                    Code="WLG",
                    Area=227755,
                    Lat=-1.8822,
                    Long=299.88,
                    Population=500000
                },
                new Region 
                {
                    Id=Guid.NewGuid(),
                    Name="Aukland",
                    Code="AUCK",
                    Area=227755,
                    Lat=-1.8822,
                    Long=299.88,
                    Population=500000

                }

            };  */


            //not a static list, getting data from database 
            //return DTO Regions
            /*var regionsDTO=new  List<Models.DTO.Region>();
            regions.ToList().ForEach(region =>
            {
                var regionDTO = new Models.DTO.Region()
                {
                    Id = region.Id,
                    Code=region.Code,
                    Name=region.Name,
                    Area=region.Area,
                    Lat=region.Lat,
                    Long=region.Long,
                    Population=region.Population,
                };
                regionsDTO.Add(regionDTO);

            });*/

            var regionsDTO= mapper.Map<List<Models.DTO.Region>>(regions);
            
            return Ok(regionsDTO);       

        }
        
    }
}
