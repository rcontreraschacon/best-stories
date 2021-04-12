using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly StoryContext _context;

        public StoryRepository(StoryContext context)
        {
            _context = context;
        }

        public async Task<Story> AddAsync(Story story)
        {
            await _context.AddAsync(story);

            _context.SaveChanges();
            return story;
        }
        public async Task<Story> GetStoryAsync(long storyId)
        {
            return await _context.Stories.FindAsync(storyId);
        }

        public async Task<List<Story>> GetStoriesAsync()
        {
            return await _context.Stories.ToListAsync();
        }

        public async Task<List<Story>> GetStoriesOrderByDescendingScoreAsync()
        {
            return await _context.Stories.OrderByDescending(s => s.Score).ToListAsync();
        }

        public async Task<Story> RemoveAsync(long storyId)
        {
            var story = await _context.Stories.FindAsync(storyId);

            if (story != null)
                _context.Stories.Remove(story);

            _context.SaveChanges();

            return story;
        }

        public async Task RemoveAllAsync()
        {
            var stories = await _context.Stories.ToListAsync();

            foreach (var story in stories)
            {
                _context.Stories.Remove(story);
            }

            _context.SaveChanges();
        }

        public async Task<Story> UpdateAsync(Story story)
        {
            var storyToUpdate = await _context.Stories.FindAsync(story.Id);

            if (storyToUpdate != null)
            {
                storyToUpdate = story;
                _context.Stories.Update(storyToUpdate);
            }

            _context.SaveChanges();

            return storyToUpdate;
        }
    }
}
