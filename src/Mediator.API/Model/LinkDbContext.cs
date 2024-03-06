using Microsoft.EntityFrameworkCore;

namespace Mediator.API.Model;

public class LinkDbContext(DbContextOptions<LinkDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Link> Links { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.OriginalUrl).IsRequired().HasMaxLength(255);
            entity.Property(l => l.ShortenedUrl).IsRequired().HasMaxLength(100);
            entity.HasOne(l => l.User)
                .WithMany(u => u.Links)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}