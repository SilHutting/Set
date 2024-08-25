using Set.Models;
using Set.Dtos;

namespace Set.Services
{
    public interface IPlayService
    {
        public List<Card> GetHint(int gameId);
        public Game? TrySet(int gameId, SetTryDto setTryDto);
        
    }
}