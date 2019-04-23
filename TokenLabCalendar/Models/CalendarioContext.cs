using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TokenLabCalendar.Models
{
    public class CalendarioContext : IdentityDbContext<ApplicationUser>
    {
        public CalendarioContext(DbContextOptions<CalendarioContext> options) : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Profile>()
                .HasMany(c => c.Events)
                .WithOne(e => e.Profile)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(c => c.Profile)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey<Profile>(b => b.ApplicationUserForeignKey);
        }
    }
}