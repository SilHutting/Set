using Set.Models;
namespace Set.Dtos
{
    public class GameReadDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public Card[]? TableCards { get; set; }
        public Deck? Deck { get; set; }
        public Card[]? Sets { get; set; }
        public int Score { get; set; }
        public bool GameOver { get; set; }
    }
}