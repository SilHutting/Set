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

    public Game GetGameById(int id)
    {
        var game =  _context.Game.Include(g => g.TableCards)
                            .Include(g => g.Deck)
                            .ThenInclude(d => d.Cards)
                            .FirstOrDefault(g => g.Id == id); 
        if (game == null)
        {
            return null;
        }
        
        game.Deck.Cards = game.Deck.Cards.Where(card => card.Id != 0).ToList();
        game.TableCards = _context.Card.Where(c => c.GameId == id && c.Id != 0).ToList();
        return game;

    }

    public async Task<List<Game>> GetAllGames()
    {
        return await _context.Game.ToListAsync();
    }

    public void CreateGame(Game newGame)
    {
        _context.Game.Add(newGame);
    }

    public void DeleteGame(Game game)
    {
        _context.Game.Remove(game);
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