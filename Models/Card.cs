namespace CardApi.Models;

public class Card
{
    public long Id { get; set; }
    public Shape shape { get; set;}
    public Fill fill { get; set;}
    public Color color { get; set;}
        public override string ToString()
    {
        return $"{color} {fill} {shape}";
    }

    public string toFileName()
    {
        return $"{shape}_{fill}_{color}.png";
    }
}

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


