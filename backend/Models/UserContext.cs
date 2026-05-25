using Microsoft.EntityFrameworkCore;

namespace backend.Models {
    public class UserContext : DbContext {
        public UserContext() { }
        public UserContext(DbContextOptions<UserContext> options) : base (options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1434;Database=UserDB;User Id=sa;Password=Cybersoft@123;TrustServerCertificate=True;");
        }

        public DbSet<User> Users { get; set; }
    }
}