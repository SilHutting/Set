using Set.Models;

namespace Set.Data
{
    public class DeckRepo : iDeckRepo
    {
        private Card[] Cards { get; set; }

        public DeckRepo()
        {
            Cards = new Card[81];
            int index = 0;
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    for (int k = 1; k <= 3; k++)
                    {
                        for (int l = 1; l <= 3; l++)
                        {
                            Cards[index] = new Card((Shape)i, (Fill)j, (Color)k, l);
                            index++;
                        }
                    }
                }
            }
        }
        public DeckRepo(Card[] cards)
        {
            Cards = cards;
        }
        public void AddCard(Card card)
        {
            throw new NotImplementedException();
        }

        public Card DrawCard()
        {
            // Get random card
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