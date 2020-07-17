using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using linkedinproject.Models;

namespace linkedinproject.Models
{
    public class LinkedInProjectDataContext : IdentityDbContext<AppUser>
    {
        public LinkedInProjectDataContext(DbContextOptions<LinkedInProjectDataContext> options) : base(options)
        {

        }
        public DbSet<linkedinproject.Models.Employee> Employee { get; set; }
        public DbSet<linkedinproject.Models.Employer> Employer { get; set; }
        public DbSet<linkedinproject.Models.Oglas> Oglas { get; set; }
        public DbSet<linkedinproject.Models.Interest> Interest { get; set; }
        public DbSet<linkedinproject.Models.Apliciraj> Apliciraj { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Interest>()
                    .HasOne<Employee>(p => p.Employee)
                    .WithMany(p => p.Interests)
                    .HasForeignKey(p => p.EmployeeId);

            modelBuilder.Entity<Interest>()
                    .HasOne<Employer>(p => p.Employer)
                    .WithMany(p => p.Interests)
                    .HasForeignKey(p => p.EmployerId);

            modelBuilder.Entity<Oglas>()
                    .HasOne<Employee>(p => p.Employee)
                    .WithMany(p => p.Oglasi)
                    .HasForeignKey(p => p.EmployeeId);

            modelBuilder.Entity<Oglas>()
                    .HasOne<Employer>(p => p.Employer)
                    .WithMany(p => p.Oglasi)
                    .HasForeignKey(p => p.EmployerId);

            modelBuilder.Entity<Apliciraj>()
                    .HasOne<Employee>(p => p.Employee)
                    .WithMany(p => p.Aplikacii)
                    .HasForeignKey(p => p.EmployeeId);

            modelBuilder.Entity<Apliciraj>()
                    .HasOne<Oglas>(p => p.Oglas)
                    .WithMany(p => p.Aplikacii)
                    .HasForeignKey(p => p.OglasId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
