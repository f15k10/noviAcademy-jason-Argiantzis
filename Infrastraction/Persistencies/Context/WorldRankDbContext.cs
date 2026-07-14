using Microsoft.EntityFrameworkCore;
using WorldRank.Domain.Entity;

namespace WorldRank.Infrastructure.Persistencies.Context
{
    public partial class WorldRankDbContext : DbContext
    {
        public WorldRankDbContext(DbContextOptions<WorldRankDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players => Set<Player>();

        public DbSet<Wallet> Wallets => Set<Wallet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(p => p.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(p => p.Score)
                    .HasColumnName("score");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("wallet");

                entity.HasKey(w => w.PlayerId);

                entity.Property(w => w.PlayerId)
                    .HasColumnName("PlayerId");

                entity.Property(w => w.Balance)
                    .HasColumnName("Balance")
                    .HasColumnType("decimal(18,2)");

                entity.Property(w => w.Currency)
                    .HasColumnName("Currency")
                    .HasConversion<int>();

                entity.Property(w => w.IsBlocked)
                    .HasColumnName("IsBlocked");

                entity.HasOne<Player>()
                    .WithOne()
                    .HasForeignKey<Wallet>(w => w.PlayerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}