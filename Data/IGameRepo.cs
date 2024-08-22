using Set.Models;

namespace Set.Data


{
    public interface IGameRepo
    {
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGameById(int id);
        void CreateGame(Game game);
        void DeleteGame(Game game);

        bool SaveChanges();
    }
}