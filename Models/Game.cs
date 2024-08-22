namespace Set.Models;
using System.ComponentModel.DataAnnotations;

public class Game
{
    [Key]
    [Required]
    public long Id { get; set; }
    public virtual List<Card> TableCards { get; set; }
    public Deck Deck { get; set; }
    // Found sets (3 cards each)
    //public List<Card> Sets { get; set; }
    public int Score { get; set; }
    public bool GameOver { get; set; }

    public Game()
    {
        TableCards = new List<Card>(12);
        Deck = new Deck();
        //Sets = new List<Card>();
        Score = 0;
        GameOver = false;

        while (true)
        {
            // Fill game with random cards by drawing from deck
            for (int i = 0; i < 12; i++)
            {
                TableCards.Add(Deck.DrawCard());
            }
            if (!TableSetPossible())
            {
                for (int i = 0; i < 12; i++)
                {
                    Deck.PutCardBack(TableCards[i]);
                }
                TableCards.Clear();
            }
            else
            {
                break;
            }
        }
    }

    // Old non-backtracking implementation. TODO: Remove
    public bool TableSetPossible()
    {
        return FindSets().Count > 0;
    }

    // Check for victory
    public bool CheckVictory()
    {
        // We diverge from normal game rules, so take note.
        // Victory is achieved when the deck is empty AND there are less than 12 cards on the table.
        if (Deck.Cards.Count == 0 && TableCards.Count < 12)
        {
            return true;
            // Victory is also achieved when the deck is empty AND there are 12 cards on the table, but no set is possible.
        }
        else if (Deck.Cards.Count == 0 && TableCards.Count == 12 && !TableSetPossible())
        {
            return true;
        }
        return false;
    }
/*
    // TODO process set (remove cards from table, add new cards, update score)
    public void ProcessSet(Card card1, Card card2, Card card3)
    {
        if (card1.IsSet(card2, card3))
        {
            RemoveCards(card1, card2, card3);
            Sets.Add(card1);
            Sets.Add(card2);
            Sets.Add(card3);
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
*/
    // Determine all possible Sets of cards on the table using backtracking algorithm
    public List<List<Card>> FindSets()
    {
        return FindSetsRecursive(new List<Card>(), 0);
    }

    private List<List<Card>> FindSetsRecursive(List<Card> currentSet, int index)
    {
        List<List<Card>> sets = new List<List<Card>>();

        if (currentSet.Count == 3)
        {
            if (IsValidSet(currentSet))
            {
                sets.Add(new List<Card>(currentSet));
            }
            return sets;
        }

        for (int i = index; i < TableCards.Count; i++)
        {
            currentSet.Add(TableCards[i]);
            sets.AddRange(FindSetsRecursive(currentSet, i + 1));
            currentSet.RemoveAt(currentSet.Count - 1);
        }

        return sets;
    }

    // Remove cards from table
    public void RemoveCards(Card card1, Card card2, Card card3)
    {
        TableCards = TableCards.Where(card => card != card1 && card != card2 && card != card3).ToList();
    }

    // Add new cards to table
    public void AddCards()
    {
        for (int i = 0; i < 3; i++)
        {
            TableCards.Add(Deck.DrawCard());
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

    private static bool IsValidSet(List<Card> cards)
    {
        return IsFeatureValid(cards[0].Number, cards[1].Number, cards[2].Number) &&
                IsFeatureValid((int)cards[0].Shape, (int)cards[1].Shape, (int)cards[2].Shape) &&
                IsFeatureValid((int)cards[0].Fill, (int)cards[1].Fill, (int)cards[2].Fill) &&
                IsFeatureValid((int)cards[0].Color, (int)cards[1].Color, (int)cards[2].Color);
    }

    private static bool IsFeatureValid(int a, int b, int c)
    {
        // Aspects of a feature are either all the same or all different
        return (a == b && b == c) || (a != b && b != c && a != c);
    }
}