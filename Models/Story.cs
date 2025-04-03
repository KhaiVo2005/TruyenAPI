namespace TruyenAPI.Models
{
    public class Story
    {
        public Guid StoryID {  get; set; }
        public string StoryName { get; set; }
        public string StoryDescription { get; set; }
        public byte[] StoryImage { get; set; }
        public ICollection<Chapter> Chapters { get; set; }
    }
}
