using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TruyenAPI.Data;
using TruyenAPI.Models;
using TruyenAPI.Models.DTOs;

namespace TruyenAPI.Repositories.ChapterImageRes
{
    public class ChapterImageRespository:IChapterImages
    {
        TruyenDbContext _context;
        IMapper mapper;

        public ChapterImageRespository(TruyenDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<List<ChapterImage>> GetAllAsync() => await _context.chapterImages.ToListAsync();
        public async Task<ChapterImage> GetAsync(Guid id) => await _context.chapterImages.FindAsync(id) ?? new ChapterImage();
        public async Task<ChapterImageDTO> Create(ChapterImageDTO chapterImageDTO)
        {
            ChapterImage chapterImage = mapper.Map<ChapterImage>(chapterImageDTO);
            await _context.chapterImages.AddAsync(chapterImage);
            await _context.SaveChangesAsync();
            return chapterImageDTO;
        }
        public async Task<ChapterImage> Update(Guid id, ChapterImageDTO chapterImageDTO)
        {
            ChapterImage chapterImage = _context.chapterImages.Find(id) ?? new ChapterImage();
            if (chapterImage == null) return new ChapterImage();
            mapper.Map(chapterImageDTO, chapterImage);
            await _context.SaveChangesAsync();
            return chapterImage;
        }
        public async Task<ChapterImage> DeleteAsync(Guid id)
        {
            ChapterImage chapterImage = await _context.chapterImages.FindAsync(id) ?? new ChapterImage();
            if (chapterImage == null) return new ChapterImage();
            _context.chapterImages.Remove(chapterImage);
            await _context.SaveChangesAsync();
            return chapterImage;
        }
    }
}
