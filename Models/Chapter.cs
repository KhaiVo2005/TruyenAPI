namespace TruyenAPI.Models
{
    public class Chapter
    {
        public Guid ChapterID { get; set; }
        public int ChapterNumber { get; set; }
        public Guid StoryID {  get; set; }
        public ICollection<ChapterImage> ChapterImages { get; set; }
    }
}
