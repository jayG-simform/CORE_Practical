using Microsoft.EntityFrameworkCore;
using Practical17.Models;

namespace Practical17.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options):base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(new Roles()
            {
                Id = 1,
                UserRole="Admin"
            },
            new Roles()
            {
                Id = 2,
                UserRole = "User"
            });
            modelBuilder.Entity<Users>().HasData(new Users()
            {
                Id = 4,
                FirstName = "Admin",
                LastName = "Jay",
                Email = "Admin@gmail.com",
                Address = "Ahemdabad, Gujrat",
                MobileNumber = "7383751559",
                Password = "Admin@123"
            });

            modelBuilder.Entity<UserRoles>().HasData(new UserRoles()
            {
                Id = 1,
                UserId = 4,
                RoleId = 1
            });
        }

    }
}
