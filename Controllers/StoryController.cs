using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruyenAPI.Data;
using TruyenAPI.Models;
using TruyenAPI.Models.DTOs;
using TruyenAPI.Repositories.StoryRes;

namespace TruyenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoryController : ControllerBase
    {
        private TruyenDbContext dbContext;
        private IStoryRepository storyRespository;
        private IMapper mapper;

        public StoryController(TruyenDbContext dbContext, IStoryRepository storyRespository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.storyRespository = storyRespository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? search)
        {
            return Ok(await storyRespository.GetAllAsync(search));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var story = await storyRespository.GetAsync(id);
            if (story == null) return BadRequest();
            return Ok(story);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoryDTO storyAdd)
        {
            Story? story = await storyRespository.CreateAsync(storyAdd);
            if (story == null) return BadRequest();
            return Ok(storyAdd);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id ,StoryDTO storyDTO)
        {
            var story = await storyRespository.UpdateAsync(id, storyDTO);
            if (story == null) return BadRequest(); 
            return Ok(mapper.Map<StoryDTO>(story));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Story? story = await storyRespository.DeleteAsync(id);
            if (story == null) return BadRequest();
            return Ok(story);
        }

    }
}
