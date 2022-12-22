using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{ 

    [ApiController]//attribute
    [Route("[Controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        public WalksController(IWalkRepository walkRepository,IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        //creating controller for getallwalks
        [HttpGet]
        public async Task<IActionResult>GetAllWalksAsync()
        {
            //Fetch data from database to domain walks
            var walksDomain=await walkRepository.GetAllAsync();
            //convert domain walks to DTO walks
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);
            //Return response
            return Ok(walksDTO);

        }
        
        //creating controller for Getwalk by Id
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAllWalksAsync")]
        public async Task<IActionResult>GetWalkAsync(Guid id)
        {
            //get walkdomain obj from database
            var walkDomain = await walkRepository.GetAsync(id);
            //Convert domain obj to DTO
            var walkDTO=mapper.Map<Models.DTO.Walk>(walkDomain);
            //return response
            return Ok(walkDTO);
        }
       
        //creating controller for addwalk
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            //convert DTO to domain object
            var walkDomain = new Models.Domain.Walk()
            {
                Length=addWalkRequest.Length,
                Name=addWalkRequest.Name,
                RegionId=addWalkRequest.RegionId,
                WalkDifficultyId=addWalkRequest.WalkDifficultyId
            };
            //pass domain object to repository to persist this
            walkDomain=await walkRepository.AddAsync(walkDomain);
            //convert domain object back to DTO
            var walkDTO=new Models.DTO.Walk
            {
                Id=walkDomain.Id,
                Length=walkDomain.Length,
                Name=walkDomain.Name,
                RegionId=walkDomain.RegionId,   
                WalkDifficultyId=walkDomain.WalkDifficultyId

            };
            //send DTO response back to client
            return CreatedAtAction(nameof(GetAllWalksAsync), new { id = walkDTO.Id }, walkDTO);

        }

        //creating controller for updatewalk
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute]Guid id, 
            [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            //convert dto to domain object 
            var walkDomain = new Models.Domain.Walk()
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId

            };

            //pass details to repository-get domain obj in response(or null)
            walkDomain = await walkRepository.UpdateAsync(id,walkDomain);
            
            //handle null not found
            if(walkDomain==null)
            {
                return NotFound();
            }
            else
            {
                //convert back domain to DTO 
                var WalkDTO = new Models.DTO.Walk
                {

                    Id = walkDomain.Id,
                    Length = walkDomain.Length,
                    Name = walkDomain.Name,
                    RegionId = walkDomain.RegionId,
                    WalkDifficultyId = walkDomain.WalkDifficultyId
                };
                //Return Response
                return Ok(WalkDTO);


            }



        }

        //creating controller for deletewalk
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult>DeleteWalkAsync(Guid id)
        {
            //call repository to dalete walk
            var walkDomain=await walkRepository.DeleteAsync(id);
            if (walkDomain == null) 
            {
                return NotFound(); 
            }
            var walkDTO=mapper.Map<Models.DTO.Walk>(walkDomain);    
            return Ok(walkDTO);
        }








    }
}
