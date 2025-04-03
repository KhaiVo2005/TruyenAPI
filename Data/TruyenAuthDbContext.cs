using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TruyenAPI.Data
{
    public class TruyenAuthDbContext : IdentityDbContext
    {
        public TruyenAuthDbContext(DbContextOptions<TruyenAuthDbContext> options) : base(options)
        {
        }
    }
}
