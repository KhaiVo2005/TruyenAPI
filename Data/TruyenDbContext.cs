using Microsoft.EntityFrameworkCore;
using TruyenAPI.Models;

namespace TruyenAPI.Data
{
    public class TruyenDbContext: DbContext
    {
        public TruyenDbContext(DbContextOptions<TruyenDbContext> dbContext) : base(dbContext)
        { 
        }
        public DbSet<ChapterImage> chapterImages { get; set; }
        public DbSet<Chapter> chapters { get; set; }
        public DbSet<Story> stories { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Chapter>().HasOne(s => s.Story).WithMany().HasForeignKey(s => s.StoryID).OnDelete(DeleteBehavior.Cascade);
        //    modelBuilder.Entity<ChapterImage>().HasOne(c => c.Chapter).WithMany().HasForeignKey(c => c.ChapterID).OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
