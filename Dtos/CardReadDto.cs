using Set.Models;
namespace Set.Dtos
{
    public class CardReadDto
    {
        public long Id { get; set; }
        public Shape Shape { get; set; }
        public Fill Fill { get; set; }
        public Color Color { get; set; }
        public int Number { get; set; }
        public int TestId { get; set; }
    }
}