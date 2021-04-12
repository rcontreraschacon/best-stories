using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IStoriesAccessor
    {
        Task<ICollection<Story>> GetBestStoriesAsync(CancellationToken cancellationToken);
    }
}