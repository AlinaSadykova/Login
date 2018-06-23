using Microsoft.EntityFrameworkCore;
 
namespace Login.Models
{
    public class UserContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<Eusers> eusers { get; set; }
        public DbSet<Activities> activities { get; set; }
        public DbSet<Guestlist> guestlist{ get; set; }

        

    }
}