using TruyenAPI.Models;
using TruyenAPI.Models.DTOs;

namespace TruyenAPI.Repositories.ChapterRes
{
    public interface IChapterRespository
    {
        Task<List<Chapter>> GetAllAsync();
        Task<Chapter> GetByIdAsync(Guid id);
        Task<ChapterDTO> CreateAsync(ChapterDTO chapterDTO);
        Task<Chapter> UpdateAsync(Guid id, ChapterDTO chapterDTO);
        Task<Chapter> DeleteAsync(Guid id);
    }
}
