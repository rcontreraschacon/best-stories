using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Stories
{
    public class StoriesAccessor : IStoriesAccessor
    {
        private readonly HttpClient _client;
        private readonly IOptions<HackerNewsOptions> _configuration;
        private readonly ILogger<StoriesAccessor> _logger;
        private readonly IMapper _mapper;

        public StoriesAccessor(IOptions<HackerNewsOptions> configuration, IMapper mapper, ILogger<StoriesAccessor> logger)
        {
            _client = new HttpClient();
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<Story>> GetBestStoriesAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("GetBestStoriesAsync - Begin");
            List<Story> result = null;
            try
            {
                _logger.LogDebug("GetBestStoriesAsync - Before getting the best stories");
                var bestStoriesResponse = await _client.GetAsync(_configuration.Value.BestStories, cancellationToken);
                _logger.LogDebug($"GetBestStoriesAsync - Best Stories Response Status: {((int)bestStoriesResponse.StatusCode)}");
                if (bestStoriesResponse.StatusCode == HttpStatusCode.OK)
                {
                    string bestStoriesContent = (bestStoriesResponse.Content != null) ? (await bestStoriesResponse.Content.ReadAsStringAsync().ConfigureAwait(false)) : null;
                    _logger.LogDebug($"GetBestStoriesAsync - Best Stories Response Content: {bestStoriesContent}");

                    var stories = JsonConvert.DeserializeObject<long[]>(bestStoriesContent);
                    if (stories != null && stories.Length > 0)
                    {
                        _logger.LogDebug("GetBestStoriesAsync - Before getting the items from the stories array");
                        var items = await GetItemsAsync(stories, cancellationToken);
                        result = new List<Story>(items);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetBestStoriesAsync - Exception: {ex}");
                throw;
            }

            _logger.LogDebug("GetBestStoriesAsync - End");
            return result;
        }

        private async Task<HashSet<Story>> GetItemsAsync(long[] storyIds, CancellationToken cancellationToken)
        {
            var storiesSet = new HashSet<Story>();
            foreach (long storyId in storyIds)
            {
                _logger.LogDebug("GetItemsAsync - Before getting the item");
                var itemResponse = await _client.GetAsync($"{_configuration.Value.Item}/{storyId}.json", cancellationToken);
                if (itemResponse.StatusCode == HttpStatusCode.OK)
                {
                    string itemContent = (itemResponse.Content != null) ? (await itemResponse.Content.ReadAsStringAsync().ConfigureAwait(false)) : null;
                    _logger.LogDebug($"GetItemsAsync - Story Response Content: {itemContent}");
                    var storyDto = JsonConvert.DeserializeObject<StoryDto>(itemContent);
                    if (storyDto != null)
                    {
                        var story = _mapper.Map<StoryDto, Story>(storyDto);
                        storiesSet.Add(story);
                    }
                }
            }

            return storiesSet;
        }
    }
}