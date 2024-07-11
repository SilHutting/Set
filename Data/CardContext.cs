using Microsoft.EntityFrameworkCore;
using Set.Models

namespace Set.Data;

public class CardContext : DbContext
{
    public CardContext(DbContextOptions<CardContext> options)
        : base(options)
    {
    }

    public DbSet<Card> Card { get; set; } = null!;
}