using Avalonia.Controls;

namespace Sudoku.Views.Controls;

public partial class BigCell : UserControl
{
    public BigCell()
    {
        InitializeComponent();
        NumberCells = new NumberCell[3, 3];
        NumberCells[0, 0] = NumberCell00;
        NumberCells[1, 0] = NumberCell10;
        NumberCells[2, 0] = NumberCell20;
        NumberCells[0, 1] = NumberCell01;
        NumberCells[1, 1] = NumberCell11;
        NumberCells[2, 1] = NumberCell21;
        NumberCells[0, 2] = NumberCell02;
        NumberCells[1, 2] = NumberCell12;
        NumberCells[2, 2] = NumberCell22;
    }

    public NumberCell[,] NumberCells { get; }
}