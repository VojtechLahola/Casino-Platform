using CasinoPlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CasinoPlatform.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<GameLog> GameLogs => Set<GameLog>();
    public DbSet<ActiveGame> ActiveGames => Set<ActiveGame>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Username)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.HasIndex(u => u.Username)
                  .IsUnique();

            entity.Property(u => u.PasswordHash)
                  .IsRequired()
                  .HasMaxLength(256);

            entity.Property(u => u.Balance)
                  .HasPrecision(18, 2)
                  .HasDefaultValue(0m);

            entity.Property(u => u.CreatedAt)
                  .IsRequired();
        });

        modelBuilder.Entity<GameLog>(entity =>
        {
            entity.ToTable("GameLogs");
            entity.HasKey(g => g.Id);

            entity.Property(g => g.GameName)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(g => g.BetAmount).HasPrecision(18, 2);
            entity.Property(g => g.Payout).HasPrecision(18, 2);
            entity.Property(g => g.Multiplier).HasPrecision(18, 4);

            entity.Property(g => g.PlayedAt)
                  .IsRequired();

            entity.HasOne(g => g.User)
                  .WithMany(u => u.GameLogs)
                  .HasForeignKey(g => g.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(g => new { g.UserId, g.PlayedAt });
        });

        modelBuilder.Entity<ActiveGame>(entity =>
        {
            entity.ToTable("ActiveGames");
            entity.HasKey(a => a.Id);

            entity.Property(a => a.GameType)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(a => a.GameState)
                  .IsRequired()
                  .HasColumnType("longtext");

            entity.Property(a => a.CurrentMultiplier)
                  .HasPrecision(18, 4);

            entity.HasOne(a => a.User)
                  .WithMany(u => u.ActiveGames)
                  .HasForeignKey(a => a.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(a => new { a.UserId, a.GameType });
        });
    }
}
