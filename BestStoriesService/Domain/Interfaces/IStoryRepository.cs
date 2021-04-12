using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IStoryRepository
    {
        Task<List<Story>> GetStoriesAsync();

        Task<List<Story>> GetStoriesOrderByDescendingScoreAsync();

        Task<Story> GetStoryAsync(long itemId);

        Task<Story> AddAsync(Story item);

        Task<Story> UpdateAsync(Story item);

        Task<Story> RemoveAsync(long itemId);

        Task RemoveAllAsync();
    }
}