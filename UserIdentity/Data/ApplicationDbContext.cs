

using UserIdentity.Models;

namespace UserIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //Add new DbSet ApplicationUser
        
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
