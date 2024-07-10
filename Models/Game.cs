using Set.Models;
namespace Set.Models;


public class Game
{
    public long Id { get; set; }
    public long PlayerId { get; set; }
    public Card[] TableCards { get; set; }
    public Deck Deck { get; set; }
    // Found sets (3 cards each)
    public Card[][] Sets { get; set; }
    public int Score { get; set; }
    public bool GameOver { get; set; }
    public Game()
    {
        TableCards = new Card[12];
        Deck = new Deck();
        Sets = new Card[0][]; // Empty array
        Score = 0;
        GameOver = false;

        while(!TableSetPossible()){
            // Fill game with random cards by drawing from deck
            for (int i = 0; i < 12; i++)
            {
                TableCards[i] = Deck.DrawCard();
            }
            if (!TableSetPossible())
            {
                for (int i = 0; i < 12; i++)
                {
                    Deck.PutCardBack(TableCards[i]);
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
                    if (TableCards[i].IsSet(TableCards[j], TableCards[k]))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }



    // Check for victory
    public bool CheckVictory()
    {
        // We diverge from normal game rules, so take note.
        // Victory is achieved when the deck is empty AND there are less than 12 cards on the table.
        if (Deck.Cards.Length == 0 && TableCards.Length < 12)
        {
            return true;
            // Victory is also achieved when the deck is empty AND there are 12 cards on the table, but no set is possible.
        } else if(Deck.Cards.Length == 0 && TableCards.Length == 12 && !TableSetPossible()){
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
    
    // Determine all possible Sets of cards on the table using backtracking algorithm
    public List<Card[]> FindAllSets()
    {
        List<Card[]> sets = new List<Card[]>();
        FindSets(new List<Card>(), 0, sets);
        return sets;
    }

    private void FindSets(List<Card> currentSet, int startIndex, List<Card[]> sets)
    {
        if (currentSet.Count == 3)
        {
            sets.Add(currentSet.ToArray());
            return;
        }

        for (int i = startIndex; i < TableCards.Length; i++)
        {
            currentSet.Add(TableCards[i]);
            FindSets(currentSet, i + 1, sets);
            currentSet.RemoveAt(currentSet.Count - 1);
        }
    }

    // remove cards from table
    public void RemoveCards(Card card1, Card card2, Card card3)
    {
        TableCards = TableCards.Where(card => card != card1 && card != card2 && card != card3).ToArray();
    }

    // Add new cards to table
    public void AddCards()
    {
        for (int i = 0; i < 3; i++)
        {
            TableCards.Append(Deck.DrawCard());
        }
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
                    if (TableCards[i].IsSet(TableCards[j], TableCards[k]))
                    {
                        return new Card[] { TableCards[i], TableCards[j], TableCards[k] };
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


