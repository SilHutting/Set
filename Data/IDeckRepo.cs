using Set.Models;

namespace Set.Data
{
    public interface iDeckRepo
    {
        // Declare the methods for the Deck repository
        void AddCard(Card card);
        Card DrawCard();
        IEnumerable<Card> GetAllCards();
        Card GetCardById(int id);
    }
}