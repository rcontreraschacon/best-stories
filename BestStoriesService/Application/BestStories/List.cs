using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.BestStories
{
    public class List
    {
        public class Query : IRequest<List<StoryDto>>
        {

        }

        public class QueryHandler : IRequestHandler<Query, List<StoryDto>>
        {
            private readonly IStoryRepository _storyRepository;
            private readonly IMapper _mapper;

            public QueryHandler(IStoryRepository storyRepository, IMapper mapper)
            {
                _storyRepository = storyRepository;
                _mapper = mapper;
            }

            public async Task<List<StoryDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var stories = await _storyRepository.GetStoriesOrderByDescendingScoreAsync();
                return _mapper.Map<List<Story>, List<StoryDto>>(stories);
            }
        }
    }
}