using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TruyenAPI.Models;
using TruyenAPI.Models.DTOs;
using TruyenAPI.Repositories.ChapterRes;

namespace TruyenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : Controller
    {
        private readonly IChapterRespository respository;
        private readonly IMapper mapper;

        public ChapterController(IChapterRespository respository, IMapper mapper)
        {
            this.respository = respository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await respository.GetAllAsync());
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get(Guid id)
        {
            Chapter chapter = await respository.GetByIdAsync(id);
            if (chapter == null) return BadRequest();
            return Ok(chapter);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ChapterDTO chapterDTO)
        {
            ChapterDTO chapter = await respository.CreateAsync(chapterDTO);
            return Ok(chapter);
        }
        [HttpPut("{id:guid}")]
        [Authorize( Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, ChapterDTO chapterDTO)
        {
            Chapter chapter = await respository.UpdateAsync(id, chapterDTO);
            if (chapter == null) return BadRequest();
            return Ok(chapter);
        }
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Chapter chapter = await respository.DeleteAsync(id);
            if (chapter == null) return BadRequest();
            return Ok(chapter);
        }
    }
}
