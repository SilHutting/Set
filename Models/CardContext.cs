using Microsoft.EntityFrameworkCore;

namespace CardApi.Models;

public class CardContext : DbContext
{
    public CardContext(DbContextOptions<CardContext> options)
        : base(options)
    {
    }

    public DbSet<Card> Card { get; set; } = null!;
}