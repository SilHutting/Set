namespace Set.Models;
using System.ComponentModel.DataAnnotations;

public class Deck
{
    [Key]
    [Required]
    public long Id { get; set; }
    public List<Card> Cards { get; set; }
    public Deck()
    {
        Cards = new List<Card>(81);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        int row = (i*27) + (j*9) + (k*3) + l;
                        Cards.Add(new Card((Shape)i, (Fill)j, (Color)k, (int)l));
                    }
                }
            }
        }
    }

    // Draw cards from deck
    public Card DrawCard()
    {
        int length = Cards.Count;
        int suggestedCard = new Random().Next(length);
        Card card = Cards[suggestedCard];
        // Remove card from deck
        Cards.RemoveAt(suggestedCard);

        return card;
    }
    public void PutCardBack(Card card)
    {
        _ = Cards.Append(card);
    }
    
}