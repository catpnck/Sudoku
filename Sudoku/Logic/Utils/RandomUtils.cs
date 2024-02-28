using System;
using Sudoku.Model;

namespace Sudoku.Logic.Utils;

public static class RandomUtils
{
    private static readonly Random Random = new();

    public static (int, int) NextTwoFromOneGroup(Dimension dimension)
    {
        var max = dimension.SmallDimension;
        var group = Random.Next(max);
        var firstNum = Random.Next(max);
        var secondNum = Random.Next(max);
        while (firstNum == secondNum)
            secondNum = Random.Next(max);

        return (firstNum + group * dimension.SmallDimension, secondNum + group * dimension.SmallDimension);
    }

    public static (int, int) NextTwo(Dimension dimension)
    {
        var max = dimension.SmallDimension;
        var firstNum = Random.Next(max);
        var secondNum = Random.Next(max);
        while (firstNum == secondNum)
            secondNum = Random.Next(max);

        return (firstNum, secondNum);
    }

    public static T Choose<T>(T[] items)
    {
        var i = Random.Next(items.Length);
        return items[i];
    }

    public static Cell ChooseCell(Board board)
    {
        Cell? cell = null;
        while (cell == null || !cell.Number.HasValue)
        {
            var i = Random.Next(board.Dimension.FullDimension);
            var j = Random.Next(board.Dimension.FullDimension);

            cell = board.Cells[i, j];
        }

        return cell;
    }
}