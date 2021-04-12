using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISynchronizer
    {
        Task SynchronizeAsync(CancellationToken cancellationToken);
    }
}