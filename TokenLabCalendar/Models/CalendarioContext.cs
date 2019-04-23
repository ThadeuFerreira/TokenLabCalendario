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

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>()
                .HasMany(c => c.Events)
                .WithOne(e => e.Usuario)
                .OnDelete(DeleteBehavior.SetNull); ;
        }
    }
}