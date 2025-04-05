using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruyenAPI.Models;
using TruyenAPI.Models.DTOs;
using TruyenAPI.Repositories.ChapterImageRes;

namespace TruyenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChapterImageController : Controller
    {
        IChapterImages _context;
        IMapper mapper;
        public ChapterImageController(IChapterImages chapter, IMapper mapper)
        {
            _context = chapter;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.GetAllAsync());
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            ChapterImage chapterImage = await _context.GetAsync(id);
            if (chapterImage == null) return BadRequest();
            return Ok(chapterImage);
        }
        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> Create(ChapterImageDTO chapterImageDTO)
        {
            return Ok(await _context.Create(chapterImageDTO));
        }
        [HttpPut("{id:guid}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Upload(Guid id, ChapterImageDTO chapterImageDTO)
        {
            ChapterImage chapterImage = await _context.Update(id, chapterImageDTO);
            if (chapterImage == null) return BadRequest(); 
            return Ok(chapterImage);
        }
        [HttpDelete("{id:guid}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            ChapterImage chapterImage = await _context.DeleteAsync(id);
            if(chapterImage == null) return BadRequest();
            return Ok(chapterImage);
        }
    }
}
