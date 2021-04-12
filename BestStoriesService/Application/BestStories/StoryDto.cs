namespace Application.BestStories
{
    public class StoryDto
    {
        public string Title { get; set; }
        public string Uri { get; set; }
        public string PostedBy { get; set; }
        public string Time { get; set; }
        public long Score { get; set; }
        public long CommentCount { get; set; }
    }
}