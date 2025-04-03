using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruyenAPI.Data;
using TruyenAPI.Mapping;
using TruyenAPI.Models;
using TruyenAPI.Models.DTOs;

namespace TruyenAPI.Repositories.StoryRes
{
    public class StoryRespository: IStoryRepository
    {
        private readonly TruyenDbContext _storyRepository;
        private readonly IMapper _autoMapperProfile;

        public StoryRespository(TruyenDbContext truyenDbContext, IMapper autoMapperProfile)
        {
            _storyRepository = truyenDbContext;
            _autoMapperProfile = autoMapperProfile;
        }

        public async Task<List<Story>> GetAllAsync(string? search)
        {
            var story = _storyRepository.stories.Include(c => c.Chapters.OrderBy(c2 => c2.ChapterNumber)).ThenInclude(c => c.ChapterImages).AsQueryable();
            if (search == null)
            {
                return await story.ToListAsync();
            }
            return await story.Where(s => s.StoryName.Contains(search)).ToListAsync();
        }
        public async Task<Story?> GetAsync(Guid id)
        {
            return await _storyRepository.stories.Include(c => c.Chapters).ThenInclude(c => c.ChapterImages).FirstOrDefaultAsync(s => s.StoryID == id);
        }
        public async Task<Story?> CreateAsync(StoryDTO storyDTO)
        {
            Story story = _autoMapperProfile.Map<Story>(storyDTO);
            _storyRepository.stories.Add(story);
            await _storyRepository.SaveChangesAsync();
            return story;
        }
        public async Task<Story?> UpdateAsync(Guid id, StoryDTO storyDTO)
        {
            var story = await _storyRepository.stories.Include(c => c.Chapters).ThenInclude(c => c.ChapterImages).FirstOrDefaultAsync(s => s.StoryID == id);
            if (story == null) return null;
            _autoMapperProfile.Map(storyDTO, story);
            await _storyRepository.SaveChangesAsync();
            return story;
        }
        public async Task<Story?> DeleteAsync(Guid id)
        {
            var story = await _storyRepository.stories.Include(c => c.Chapters).ThenInclude(c => c.ChapterImages).FirstOrDefaultAsync(s => s.StoryID == id);
            if (story == null) return null;
            _storyRepository.chapters.RemoveRange(story.Chapters);
            _storyRepository.stories.Remove(story);
            await _storyRepository.SaveChangesAsync();
            return story;
        }
    }
}
