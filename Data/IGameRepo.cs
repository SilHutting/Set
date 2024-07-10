using Set.Models;

namespace Set.Data


{
    public interface IGameRepo
    {
        IEnumerable<Game> GetAllGames();
        Game GetGameById(int id);
        void CreateGame(Game game);
        void DeleteGame(int id);
    }
}