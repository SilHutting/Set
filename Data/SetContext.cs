using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Set.Models;

namespace Set.Data;
public class SetContext : DbContext
{
    public SetContext(DbContextOptions<SetContext> options)
        : base(options)
    {
    }
    public DbSet<Game> Game { get; set; } = null!;
    public DbSet<Deck> Deck { get; set; } = null!;
    public DbSet<Card> Card { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(e =>
            {
                e.ToTable("Game");

                e.HasKey(e => e.Id);
                e.Property(e => e.Id).ValueGeneratedOnAdd();
                //e.HasMany(e => e.Cards);
            });

            modelBuilder.Entity<Card>(e =>
            {
                e.ToTable("Card");

                e.HasKey(e => e.Id);
                e.Property(e => e.Id).ValueGeneratedOnAdd();
                e.HasOne(e => e.Game).WithMany(e => e.TableCards).HasForeignKey(e => e.GameId).OnDelete(DeleteBehavior.Cascade);
            });
        }
}