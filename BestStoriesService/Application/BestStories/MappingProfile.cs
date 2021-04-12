using AutoMapper;
using Domain.Models;

namespace Application.BestStories
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Story, StoryDto>();
        }

    }
}