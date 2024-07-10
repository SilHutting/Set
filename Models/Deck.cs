namespace Set.Models;

public class Deck
{
    public Card[] Cards { get; set; }
    public Deck()
    {
        Cards = new Card[81];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        Cards[i * 27 + j * 9 + k * 3 + l] = new Card((Shape)i, (Fill)j, (Color)k, (Number)l);
                    }
                }
            }
        }
    }

    // Draw cards from deck
    public Card DrawCard()
    {
        int suggestedCard = new Random().Next(Cards.Length);
        Card card = Cards[suggestedCard];
        // Remove card from deck
        Cards = Cards.Where((source, index) => index != suggestedCard).ToArray();
        return card;
    }
    public void PutCardBack(Card card)
    {
        Cards.Append(card);
    }
    
}