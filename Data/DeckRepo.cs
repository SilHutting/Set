using Set.Models;

namespace Set.Data
{
    public class DeckRepo : iDeckRepo
    {
        public void AddCard(Card card)
        {
            throw new NotImplementedException();
        }

        public void RemoveCard(Card card)
        {
            int suggestedCard = new Random().Next(Cards.Length);
            Card card = Cards[suggestedCard];
            // Remove card from deck
            Cards = Cards.Where((source, index) => index != suggestedCard).ToArray();
            return card;
        }

        public Card GetCardById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetAllCards()
        {
            throw new NotImplementedException();
        }
    }
}