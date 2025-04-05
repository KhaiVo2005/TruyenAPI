using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TruyenAPI.Data;
using TruyenAPI.Models;
using TruyenAPI.Models.DTOs;
using TruyenAPI.Repositories.StoryRes;

namespace TruyenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private TruyenDbContext dbContext;
        private IStoryRepository storyRespository;
        private IMapper mapper;
        private readonly ILogger<StoryController> logger;

        public StoryController(TruyenDbContext dbContext, IStoryRepository storyRespository, IMapper mapper, ILogger<StoryController> logger)
        {
            this.dbContext = dbContext;
            this.storyRespository = storyRespository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll(string? search)
        {
            var data = await storyRespository.GetAllAsync(search);
            logger.LogInformation($"Data: {JsonSerializer.Serialize(data)}");
            return Ok(data);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get(Guid id)
        {
            var story = await storyRespository.GetAsync(id);
            if (story == null) return BadRequest();
            return Ok(story);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(StoryDTO storyAdd)
        {
            Story? story = await storyRespository.CreateAsync(storyAdd);
            if (story == null) return BadRequest();
            return Ok(storyAdd);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id ,StoryDTO storyDTO)
        {
            var story = await storyRespository.UpdateAsync(id, storyDTO);
            if (story == null) return BadRequest(); 
            return Ok(mapper.Map<StoryDTO>(story));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Story? story = await storyRespository.DeleteAsync(id);
            if (story == null) return BadRequest();
            return Ok(story);
        }

    }
}
