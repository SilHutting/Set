using Microsoft.EntityFrameworkCore;
using Set.Models;

namespace Set.Data;
public class CardContext : DbContext
{
    public CardContext(DbContextOptions<CardContext> options)
        : base(options)
    {
    }

    public DbSet<Card> Card { get; set; } = null!;
}
public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}
public class GameContext : DbContext
{
    public GameContext(DbContextOptions<GameContext> options)
        : base(options)
    {
    }

    public DbSet<Game> game { get; set; } = null!;
}
public class DeckContext : DbContext
{
    public DeckContext(DbContextOptions<DeckContext> options)
        : base(options)
    {
    }

    public DbSet<Deck> deck { get; set; } = null!;
}