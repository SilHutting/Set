using CardApi.Models;

namespace Set.Models;


public class Game
{
    public long Id { get; set; }
    public long PlayerId { get; set; }
    public Card[] Cards { get; set; }
    public Card[] Deck { get; set; }
    public int Score { get; set; }
    public bool GameOver { get; set; }
    public bool[] Selected { get; set; }
    public bool[] Correct { get; set; }
    public bool[] SetFound { get; set; }
    public string[] Set { get; set; }

    public Game()
    {
        Cards = new Card[12];
        Deck = new Card[81];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        Deck[i * 27 + j * 9 + k * 3 + l] = new Card((Shape)i, (Fill)j, (Color)k, (Number)l);
                    }
                }
            }
        }
        Selected = new bool[12];
        Correct = new bool[12];
        SetFound = new bool[12];
        Set = new string[3];
        Score = 0;
        GameOver = false;

        while(!setPossible(){
            // Fill game with random cards by drawing from deck
            for (int i = 0; i < 12; i++)
            {
                Cards[i] = drawCard();
            }
            if (!setPossible())
            {
                for (int i = 0; i < 12; i++)
                {
                    putCardBack(Cards[i]);
                }
            }
        }


    }
    public bool setPossible()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = i + 1; j < 11; j++)
            {
                for (int k = j + 1; k < 12; k++)
                {
                    if (Cards[i].IsSet(Cards[j], Cards[k]))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // Draw cards from deck
    public Card drawCard()
    {
        int suggestedCard = new Random().Next(Deck.Length);
        Card card = Deck[suggestedCard];
        // Remove card from deck
        Deck = Deck.Where((source, index) => index != suggestedCard).ToArray();
        return card;
    }
    public void putCardBack(Card card)
    {
        Deck.Append(card);
    }
}
