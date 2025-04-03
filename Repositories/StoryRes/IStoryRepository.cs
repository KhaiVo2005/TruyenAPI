using Microsoft.AspNetCore.Mvc;
using TruyenAPI.Models;
using TruyenAPI.Models.DTOs;

namespace TruyenAPI.Repositories.StoryRes
{
    public interface IStoryRepository
    {
        Task<List<Story>> GetAllAsync(string? name = null);
        Task<Story?> GetAsync(Guid id);
        Task<Story?> CreateAsync(StoryDTO story);
        Task<Story?> UpdateAsync(Guid id, StoryDTO storyDTO);
        Task<Story?> DeleteAsync(Guid id);

    }
}
