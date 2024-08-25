namespace Set.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public class Game
{
    [Key]
    [Required]
    public long Id { get; set; }
    public virtual List<Card> TableCards { get; set; }
    public Deck Deck { get; set; }
    public int? NumPossibleSets { get; set; } = null;
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
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
                NumPossibleSets = FindSetsInBoard().Count;
                break;
            }
        }
    }

    /// <summary>
    /// If TRUE, board contains atleast 1 set, if FALSE, No more sets are possible
    /// </summary>
    /// <returns></returns>
    private bool TryFindAndReshuffleSets()
    {
        // No sets found in current table
        if (!TableSetPossible())
        {
            // Everything back to deck
            for (int i = 0; i < TableCards.Count; i++)
            {
                Deck.PutCardBack(TableCards[i]);
            }
            TableCards.Clear();

            // Is there a set in all playable cards?
            var allPossibleSets = FindSetsRecursive(TableCards, new List<Card>());
            if (allPossibleSets.Count == 0)
            {
                // No sets found in all remaining playable cards
                return false;
            } else {
                var firstSet = allPossibleSets[0];
                foreach (var card in firstSet)
                {
                    Deck.Cards.Remove(card);
                    TableCards.Add(card);
                }
                // Fill remaining table with random cards
                while (TableCards.Count < 12)
                {
                    TableCards.Add(Deck.DrawCard());
                }
                NumPossibleSets = FindSetsInBoard().Count;
                return true;
            }

        }else {
            NumPossibleSets = FindSetsInBoard().Count;
            return true;
            }
    }

    public bool TableSetPossible()
    {
        return FindSetsInBoard().Count > 0;
    }
    
    // Check for victory
    public bool CheckVictory()
    {
        if (!TryFindAndReshuffleSets())
        {
            return true;
            
        }// Victory is  achieved when the deck is empty AND there are 12 cards on the table, but no set is possible.
        else if (Deck.Cards.Count == 0 && TableCards.Count <= 12 && !TableSetPossible())
        {
            return true;
        }
        NumPossibleSets = FindSetsInBoard().Count;
        return false;
    }

    // Determine all possible Sets of cards on the table using backtracking algorithm
    public List<List<Card>> FindSetsInBoard()
    {
        return FindSetsRecursive(TableCards, new List<Card>());
    }

    private List<List<Card>> FindSetsRecursive(List<Card> cardsToSearch, List<Card> currentSet, int index = 0)
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

        for (int i = index; i < cardsToSearch.Count; i++)
        {
            currentSet.Add(cardsToSearch[i]);
            sets.AddRange(FindSetsRecursive(cardsToSearch, currentSet, i + 1));
            currentSet.RemoveAt(currentSet.Count - 1);
        }

        return sets;
    }

    // Add new cards to table
    public void Add3Cards()
    {
        for (int i = 0; i < 3; i++)
        {
            TableCards.Add(Deck.DrawCard());
        }
    }

    // Show hint (a set that is possible on the table) by highlighting 2 out of 3 cards
    public List<Card> Hint()
    {
        // It is guaranteed that there is at least one set on the table, so we need not check for that.
        var allPossibleSets = FindSetsInBoard();
        var firstPossibleSet = allPossibleSets[0];
        var twoCardsOfSet = new List<Card> { firstPossibleSet[0], firstPossibleSet[1] };
        return twoCardsOfSet;
    }

    public static bool IsValidSet(List<Card> cards)
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