using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Synchronizers
{
    public class Synchronizer : ISynchronizer
    {
        private readonly IStoriesAccessor _storiesAccessor;
        private readonly IStoryRepository _itemRepository;
        private readonly ILogger<Synchronizer> _logger;

        public Synchronizer(IStoriesAccessor storiesAccessor, IStoryRepository storyRepository, ILogger<Synchronizer> logger)
        {
            _storiesAccessor = storiesAccessor;
            _itemRepository = storyRepository;
            _logger = logger;
        }
        public async Task SynchronizeAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"SynchronizeAsync - Begin");
            try
            {
                var stories = await _storiesAccessor.GetBestStoriesAsync(cancellationToken);

                if (stories != null && stories.Count > 0)
                {
                    _logger.LogDebug($"SynchronizeAsync - Before getting the 20 best stories");
                    var bestStories = stories.OrderByDescending(i => i.Score).Take(20);

                    if (bestStories != null && bestStories.Count() > 0)
                    {
                        _logger.LogDebug($"SynchronizeAsync - Before removing all the stories from database");
                        await _itemRepository.RemoveAllAsync();
                        _logger.LogDebug($"SynchronizeAsync - Before inserting the new best stories");
                        foreach (var story in bestStories)
                        {
                            await _itemRepository.AddAsync(story);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"SynchronizeAsync - Exception: {ex}");
                throw;
            }

            _logger.LogInformation($"SynchronizeAsync - End");
        }
    }
}