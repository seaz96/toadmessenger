
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; } = null!;

    public DbSet<ChatEntity> Chats { get; set; } = null!;
    
    public DbSet<MessageEntity> Messages { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}