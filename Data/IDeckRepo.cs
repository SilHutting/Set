using Set.Models;

namespace Set.Data
{
    public interface iDeckRepo
    {
        // Declare the methods for the Deck repository
        void AddCard(Card card);
        void RemoveCard(Card card);
        IEnumerable<Card> GetAllCards();
        Card GetCardById(int id);
    }
}