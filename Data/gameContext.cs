using Microsoft.EntityFrameworkCore;
using Set.Models;

namespace Set.Data;

public class GameContext : DbContext
{
    public GameContext(DbContextOptions<GameContext> options)
        : base(options)
    {
    }

    public DbSet<Game> game { get; set; } = null!;
}