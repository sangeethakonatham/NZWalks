using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
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

        
        //controller method to get all regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()//change to async
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
        
        //controller method to get single region
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsyn")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if(region== null) 
            {
                return NotFound();
            }
            var regionDTO= mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        //controller method for addregion
        [HttpPost]
        public async Task<IActionResult>AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Request DTO to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area= addRegionRequest.Area,
                Lat= addRegionRequest.Lat,
                Long= addRegionRequest.Long,
                Population= addRegionRequest.Population
            };
            
            //Pass details to repository
            region=await regionRepository.AddAsync(region);

            //convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id= region.Id,
                Code=region.Code,
                Area=region.Area,
                Lat=region.Lat,
                Long=region.Long,
                Population=region.Population

            };
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);//client knows save was successfull,new resource created
        }

        //controller method for delete region
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult>DeleteRegionAsync(Guid id)
        {
            //get region from database
            var region= await regionRepository.DeleteAsync(id);
            //if null not found
            if(region==null)
            {
                return NotFound();
            }
            //convert response back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Area= region.Area,
                Code= region.Code,
                Name= region.Name,
                Lat= region.Lat,
                Long= region.Long,
                Population=region.Population
            };
            //return ok response
            return Ok(regionDTO);
        }

        //controller method for updte region
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult>UpdateRegionAsync([FromRoute]Guid id, [FromBody]Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //convert DTO to domain model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name=updateRegionRequest.Name,
                Population = updateRegionRequest.Population

            };

            //update region using respiratory
           
            region = await regionRepository.UpdateAsync(id,region);
            //if null then not found
            if(region==null)
            {
                return NotFound();
            }
            //convert domain back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Area = region.Area,
                Code = region.Code,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            //return ok response
            return Ok(regionDTO);
        }


    }
}
