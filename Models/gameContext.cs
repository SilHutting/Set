using Microsoft.EntityFrameworkCore;

namespace Set.Models;

public class GameContext : DbContext
{
    public GameContext(DbContextOptions<GameContext> options)
        : base(options)
    {
    }

    public DbSet<Game> game { get; set; } = null!;
}