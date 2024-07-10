using System.Collections.Generic;
using Set.Models;

namespace Set.Data {

public class GameRepo : IGameRepo
{
    private List<Game> games;

    public GameRepo()
    {
        games = new List<Game>();
    }

    public Game GetGameById(int id)
    {
        return games.Find(game => game.Id == id);
    }

    public List<Game> GetAllGames()
    {
        return games;
    }

    public void CreateGame(Game game)
    {
        games.Add(game);
    }

    public void DeleteGame(int id)
    {
        Game game = games.Find(g => g.Id == id);
        if (game != null)
        {
            games.Remove(game);
        }
    }

    IEnumerable<Game> IGameRepo.GetAllGames()
    {
        throw new NotImplementedException();
    }
    }
}