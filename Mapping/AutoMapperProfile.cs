using AutoMapper;
using TruyenAPI.Models;
using TruyenAPI.Models.DTOs;

namespace TruyenAPI.Mapping
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Story, StoryDTO>().ReverseMap();
            CreateMap<Chapter, ChapterDTO>().ReverseMap();
            CreateMap<ChapterImage, ChapterImageDTO>().ReverseMap();
        }
    }
}
