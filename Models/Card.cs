namespace Set.Models;
using System.ComponentModel.DataAnnotations;
public enum Shape
{
    golf,
    ovaal,
    ruit
}
public enum Fill
{
    leeg,
    vol,
    halfvol
}
public enum Color
{
    groen,
    paars,
    rood
}
public enum Status
{
    deck,
    board,
    foundSets
}
public class Card
{
    [Key]
    [Required]
    public long Id { get; set; }
    public Shape Shape { get; set;}
    public Fill Fill { get; set;}
    public Color Color { get; set;}
    public Status Status { get; set;}
    public int Number { get; set;}
    public long? GameId { get; set; }

    //public Deck? Deck { get; set; }

    public virtual Game? Game { get; set; }

    public Card(Shape shape, Fill fill, Color color, int number, Status status = Status.deck)
    {
        this.Shape = shape;
        this.Fill = fill;
        this.Color = color;
        this.Number = number;
        this.Status = status;
    }

    public override string ToString()
    {
        return $"{Shape} {Fill} {Color} {Number}";
    }

    public string toFileName()
    {
        return $"{Shape}_{Fill}_{Color}.png";
    }

    internal bool IsSet(Card card1, Card card2)
    {
        // A set is a group of three cards where, for each of the four features, the three cards are either all the same or all different.
        return (Shape == card1.Shape && Shape == card2.Shape || Shape != card1.Shape && Shape != card2.Shape && card1.Shape != card2.Shape) &&
            (Fill == card1.Fill && Fill == card2.Fill || Fill != card1.Fill && Fill != card2.Fill && card1.Fill != card2.Fill) &&
            (Color == card1.Color && Color == card2.Color || Color != card1.Color && Color != card2.Color && card1.Color != card2.Color) &&
            (Number == card1.Number && Number == card2.Number || Number != card1.Number && Number != card2.Number && card1.Number != card2.Number);
    }
}


