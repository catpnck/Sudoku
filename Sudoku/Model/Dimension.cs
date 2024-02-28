namespace Sudoku.Model;

public class Dimension
{
    public static Dimension NINE = new(3);

    private Dimension(int smallDimension)
    {
        SmallDimension = smallDimension;
        FullDimension = smallDimension * smallDimension;
    }

    public int SmallDimension { get; private set; }
    public int FullDimension { get; private set; }
}