namespace TruyenAPI.Models
{
    public class ChapterImage
    {
        public Guid ChapterImageID { get; set; }
        public byte[] Image { get; set; }
        public Guid ChapterID { get; set; }
    }
}
