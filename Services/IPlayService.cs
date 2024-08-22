using Set.Models;
using Set.Dtos;

namespace Set.Services
{
    public interface IPlayService
    {
        public Game TrySet(int gameId, SetTryDto setTryDto);
    }
}