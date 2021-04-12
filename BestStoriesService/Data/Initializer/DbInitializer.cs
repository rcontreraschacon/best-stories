using System.Threading.Tasks;
using Data.Context;

namespace Data.Initializer
{
    public static class DbInitializer
    {
        public static async Task Initialize(StoryContext context)
        {
            await context.Database.EnsureCreatedAsync();
        }
    }
}