using System;
using System.Text;

namespace Sudoku.Model;

public class Board
{
    private readonly int[] _possibleNumbers;

    public Board(Dimension dimension)
    {
        Dimension = dimension;
        Cells = new Cell[dimension.FullDimension, dimension.FullDimension];

        for (var i = 0; i < dimension.FullDimension; i++)
        for (var j = 0; j < dimension.FullDimension; j++)
            Cells[i, j] = new Cell(i, j);

        _possibleNumbers = new int[dimension.FullDimension];
        for (var i = 0; i < dimension.FullDimension; i++)
            _possibleNumbers[i] = i + 1;

        FillWithDefault();
    }

    public Dimension Dimension { get; }

    // [column, row]
    public Cell[,] Cells { get; }


    private void FillWithDefault()
    {
        var rowIndex = 0;
        for (var i = 0; i < Dimension.SmallDimension; i++)
        for (var j = 0; j < Dimension.SmallDimension; j++)
        {
            var firstIndex = j * Dimension.SmallDimension + i;
            for (var k = 0; k < _possibleNumbers.Length; k++)
            {
                var index = (firstIndex + k) % _possibleNumbers.Length;
                Cells[k, rowIndex].Number = _possibleNumbers[index];
                Cells[k, rowIndex].Solution = _possibleNumbers[index];
            }

            rowIndex++;
        }
    }


    public void Print()
    {
        var row = new StringBuilder();
        for (var i = 0; i < Cells.GetLength(1); i++)
        {
            for (var j = 0; j < Cells.GetLength(0); j++)
                row.Append(Cells[j, i]);
            Console.WriteLine(row);
            row.Clear();
        }
    }
}