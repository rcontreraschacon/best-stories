using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class StoryContext : DbContext
    {
        public StoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Story> Stories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual void DeleteAll(string tableName)
        {
            Stories.FromSql($"DELETE FROM {tableName}");
        }
    }
}