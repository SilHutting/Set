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
        private void LoadGame(int gameId)
        {
            var game = _gameRepo.GetGameById(gameId);
            _game = game;
        }

        public List<Card> GetHint(int gameId)
        {
            LoadGame(gameId);
            if (_game == null)
            {
                return null;
            }
            var hint = _game.Hint();
            this._game.Score -= 1;
            return hint;
        }
        public Game TrySet(int gameId, SetTryDto setTryDto)
        {
            LoadGame(gameId);
            if (_game == null)
            {
                return null;
            }
            var possibleSets = _game.FindSets();
            // Print out possible sets
            foreach (var set in possibleSets)
            {
                System.Console.WriteLine("PRINT: Possible Set properties: ", set[0].ToString() + ", " + set[1].ToString() + ", " + set[2].ToString());
                System.Console.WriteLine("PRINT: Possible Set: " + set[0].Id + ", " + set[1].Id + ", " + set[2].Id);
            }

            var card1 = _game.TableCards.Find(c => c.Id == setTryDto.CardIds[0]);
            var card2 = _game.TableCards.Find(c => c.Id == setTryDto.CardIds[1]);
            var card3 = _game.TableCards.Find(c => c.Id == setTryDto.CardIds[2]);
            
            if (card1 == null || card2 == null || card3 == null)
            {
                return null;
            }


            if (Game.IsValidSet([card1, card2, card3]))
            {
                _game.TableCards.Remove(card1);
                _game.TableCards.Remove(card2);
                _game.TableCards.Remove(card3);
                
                if (_game.TableCards.Count < 12 && _game.Deck.Cards.Count > 3)
                {
                    _game.Add3Cards();

                }
                if(_game.CheckVictory()) {
                    _game.GameOver = true;
                }
                
                _game.Score += 10;
                return _game;
            }else {
                _game.Score -= 1;
                return null;
            }
        }
    }
}