using System.Collections.Generic;

namespace Infrastructure.Stories
{
    public class StoryDto
    {
        public long Id { get; set; }
        public string By { get; set; }
        public long Time { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public long Score { get; set; }
        public string Title { get; set; }
        public virtual ICollection<long> Kids { get; set; } = new List<long>();
    }
}