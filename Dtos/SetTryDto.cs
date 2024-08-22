using Set.Models;
namespace Set.Dtos
{

    public class SetTryDto
    {
        // No game id, gets it from route
        
        // collection of 3 cards
        public int[] CardIds { get; set; }
    }
}