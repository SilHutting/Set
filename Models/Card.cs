namespace Set.Models;
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
public enum Number
{
    groen,
    paars,
    rood
}


public class Card
{
    public long Id { get; set; }
    public Shape shape { get; set;}
    public Fill fill { get; set;}
    public Color color { get; set;}
    public Number number { get; set;}

    public Card(Shape shape, Fill fill, Color color, Number number)
    {
        this.shape = shape;
        this.fill = fill;
        this.color = color;
        this.number = number;
    }

    public override string ToString()
    {
        return $"{shape} {fill} {color} {number}";
    }

    public string toFileName()
    {
        return $"{shape}_{fill}_{color}.png";
    }

    internal bool IsSet(Card card1, Card card2)
    {
        // A set is a group of three cards where, for each of the four features, the three cards are either all the same or all different.
        return (shape == card1.shape && shape == card2.shape || shape != card1.shape && shape != card2.shape && card1.shape != card2.shape) &&
            (fill == card1.fill && fill == card2.fill || fill != card1.fill && fill != card2.fill && card1.fill != card2.fill) &&
            (color == card1.color && color == card2.color || color != card1.color && color != card2.color && card1.color != card2.color) &&
            (number == card1.number && number == card2.number || number != card1.number && number != card2.number && card1.number != card2.number);
    }
}


