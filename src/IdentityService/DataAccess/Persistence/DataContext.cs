using Core.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; } = null!;

    public DbSet<PhotoEntity> Photos { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasOne(a => a.Photo)
            .WithOne(a => a.User)
            .HasForeignKey<PhotoEntity>(p => p.UserId);
    }
}