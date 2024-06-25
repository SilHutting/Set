using CardApi.Models;

namespace Set.Models;


public class Game
{
    public long Id { get; set; }
    public long PlayerId { get; set; }
    public Card[] Cards { get; set; }
    public Card[] Deck { get; set; }
    // Found sets (3 cards each)
    public Card[][] Sets { get; set; }
    public int Score { get; set; }
    public bool GameOver { get; set; }
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
        Sets = new Card[0][]; // Empty array
        Score = 0;
        GameOver = false;

        while(!TableSetPossible()){
            // Fill game with random cards by drawing from deck
            for (int i = 0; i < 12; i++)
            {
                Cards[i] = DrawCard();
            }
            if (!TableSetPossible())
            {
                for (int i = 0; i < 12; i++)
                {
                    PutCardBack(Cards[i]);
                }
            }
        }


    }

    public bool TableSetPossible()
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
    public Card DrawCard()
    {
        int suggestedCard = new Random().Next(Deck.Length);
        Card card = Deck[suggestedCard];
        // Remove card from deck
        Deck = Deck.Where((source, index) => index != suggestedCard).ToArray();
        return card;
    }
    public void PutCardBack(Card card)
    {
        Deck.Append(card);
    }

    // Check for victory
    public bool CheckVictory()
    {
        // We diverge from normal game rules, so take note.
        // Victory is achieved when the deck is empty AND there are less than 12 cards on the table.
        if (Deck.Length == 0 && Cards.Length < 12)
        {
            return true;
            // Victory is also achieved when the deck is empty AND there are 12 cards on the table, but no set is possible.
        } else if(Deck.Length == 0 && Cards.Length == 12 && !TableSetPossible()){
            return true;
        }
        return false;
    }

    // TODO process set (remove cards from table, add new cards, update score)
    public void ProcessSet(Card card1, Card card2, Card card3)
    {
        if (card1.IsSet(card2, card3))
        {
            RemoveCards(card1, card2, card3);
            Sets.Append(new Card[] { card1, card2, card3 });
            Score++;
            
            // Game ended?
            if (CheckVictory())
            {
                GameOver = true;
                return;
            }

            // Add new cards to table
            AddCards();
        }
    }

    // remove cards from table
    public void RemoveCards(Card card1, Card card2, Card card3)
    {
        Cards = Cards.Where(card => card != card1 && card != card2 && card != card3).ToArray();
    }

    // Add new cards to table
    public void AddCards()
    {
    }

    // Show hint (a set that is possible on the table) by highlighting 2 out of 3 cards
    public Card[] Hint()
    {
        // It is guaranteed that there is at least one set on the table, so we need not check for that.
        for (int i = 0; i < 10; i++)
        {
            for (int j = i + 1; j < 11; j++)
            {
                for (int k = j + 1; k < 12; k++)
                {
                    if (Cards[i].IsSet(Cards[j], Cards[k]))
                    {
                        return new Card[] { Cards[i], Cards[j], Cards[k] };
                    }
                }
            }
        }
        // This should never happen
        throw new Exception("No set found on table");
    }

    // Save game state
    public void Save()
    {
        throw new NotImplementedException();
    }

}
