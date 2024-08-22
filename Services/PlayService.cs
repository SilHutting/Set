using Set.Data;
using Set.Models;
using Set.Dtos;

namespace Set.Services
{
    public class PlayService : IPlayService
    {
        private readonly IGameRepo _gameRepo;
        public Game? _game;

        public PlayService(IGameRepo gameRepo)
        {
            _gameRepo = gameRepo;
            _game = null;
        }
        private async void LoadGame(int gameId)
        {
            var game = await _gameRepo.GetGameById(gameId);
            _game = game;
        }

        public Game TrySet(int gameId, SetTryDto setTryDto)
        {
            LoadGame(gameId);

            return _game;
        }
    }
}