using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TruyenAPI.Data;
using TruyenAPI.Models;
using TruyenAPI.Models.DTOs;

namespace TruyenAPI.Repositories.ChapterRes
{
    public class ChapterRespository: IChapterRespository
    {
        private readonly TruyenDbContext _context;
        private readonly IMapper mapper;

        public ChapterRespository(TruyenDbContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
        }

        public async Task<List<Chapter>> GetAllAsync() => await _context.chapters.Include(c => c.ChapterImages).ToListAsync();
        public async Task<Chapter> GetByIdAsync(Guid id) => await _context.chapters.FirstOrDefaultAsync(c => c.ChapterID == id) ?? new Chapter();
        public async Task<ChapterDTO> CreateAsync(ChapterDTO chapterDTO)
        {
            Chapter chapter = mapper.Map<Chapter>(chapterDTO);
            _context.chapters.Add(chapter);
            await _context.SaveChangesAsync();
            return chapterDTO;
        }
        public async Task<Chapter> UpdateAsync(Guid id, ChapterDTO chapterDTO)
        {
            Chapter chapter = await _context.chapters.FirstOrDefaultAsync(c => c.ChapterID == id) ?? new Chapter();
            if (chapter == null) return new Chapter();
            mapper.Map(chapterDTO, chapter);
            await _context.SaveChangesAsync();
            return chapter;
        }
        public async Task<Chapter> DeleteAsync(Guid id)
        {
            Chapter chapter = await GetByIdAsync(id);
            if (chapter == null) return new Chapter();
            _context.chapters.Remove(chapter);
            await _context.SaveChangesAsync();
            return chapter;
        }
    }
}
