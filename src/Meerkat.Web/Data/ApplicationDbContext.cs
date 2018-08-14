using Meerkat.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Meerkat.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventGroup> EventGroups { get; set; }
        public DbSet<Frame> Frames { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Event>().Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Entity<Event>().HasMany(e => e.StackTrace).WithOne(f => f.Event);

            builder.Entity<EventGroup>().HasMany(g => g.Events)
                                        .WithOne(e => e.Group)
                                        .HasForeignKey(e => e.GroupId)
                                        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
