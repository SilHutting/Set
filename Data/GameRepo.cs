using System.Collections.Generic;
using System.Linq;
using System;
using Set.Models;
using Microsoft.EntityFrameworkCore;

namespace Set.Data {

public class GameRepo : IGameRepo
{
    private readonly SetContext _context;
    private List<Game> games;

    public GameRepo(SetContext context)
    {
        _context = context;
    }

    public async Task<Game> GetGameById(int id)
    {
        return await _context.Game.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Game>> GetAllGames()
    {
        return await _context.Game.ToListAsync();
    }

    public void CreateGame(Game game)
    {
        games.Add(game);
    }

    public void DeleteGame(Game game)
    {
        games.Remove(game);
    }

    async Task<IEnumerable<Game>> IGameRepo.GetAllGames()
    {
        return await _context.Game.ToListAsync();
    }

    bool IGameRepo.SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }
}
}