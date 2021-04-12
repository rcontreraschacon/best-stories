using System;
using AutoMapper;
using Domain.Models;

namespace Infrastructure.Stories
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StoryDto, Story>()
                .ForMember(dest => dest.PostedBy, opt => opt.MapFrom(src => src.By))
                .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Kids.Count))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddMilliseconds(src.Time).ToString("yyyy-MM-ddThh:mm:ssK")));
        }
    }
}