using TruyenAPI.Models;
using TruyenAPI.Models.DTOs;

namespace TruyenAPI.Repositories.ChapterImageRes
{
    public interface IChapterImages
    {
        Task<List<ChapterImage>> GetAllAsync();
        Task<ChapterImage> GetAsync(Guid id);
        Task<ChapterImageDTO> Create(ChapterImageDTO chapterImage);
        Task<ChapterImage> Update (Guid id, ChapterImageDTO chapterImage);
        Task<ChapterImage> DeleteAsync(Guid id);
    }
}
