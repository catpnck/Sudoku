using System;
using System.Collections.Generic;
using Sudoku.Model;

namespace Sudoku.Logic;

public class BoardValidator
{
    public bool IsValid(Board board)
    {
        return IsRowsValid(board) && IsColumnsValid(board) && IsCellsValid(board);
    }

    private bool IsRowsValid(Board board)
    {
        var res = true;
        for (var i = 0; i < board.Dimension.FullDimension; i++)
        {
            var rowNum = i;
            res &= IsLineValid(board, (b, j) => b.Cells[j, rowNum]);
        }

        return res;
    }

    private bool IsColumnsValid(Board board)
    {
        var res = true;
        for (var i = 0; i < board.Dimension.FullDimension; i++)
        {
            var colNum = i;
            res &= IsLineValid(board, (b, j) => b.Cells[colNum, j]);
        }

        return res;
    }

    private bool IsLineValid(Board board, Func<Board, int, Cell> getCellFunc)
    {
        var set = new HashSet<int>();
        for (var i = 0; i < board.Dimension.FullDimension; i++)
        {
            var num = getCellFunc(board, i).Number;
            if (!num.HasValue)
                continue;
            if (!set.Add(num.Value))
                return false;
        }

        return true;
    }

    private bool IsCellsValid(Board board)
    {
        var res = true;
        for (var y = 0; y < board.Dimension.SmallDimension; y++)
        for (var x = 0; x < board.Dimension.SmallDimension; x++)
            res &= IsCellValid(board, y, x);

        return res;
    }

    private bool IsCellValid(Board board, int y, int x)
    {
        var set = new HashSet<int>();
        for (var i = 0; i < board.Dimension.SmallDimension; i++)
        for (var j = 0; j < board.Dimension.SmallDimension; j++)
        {
            var num = board.Cells[y * board.Dimension.SmallDimension + i, x * board.Dimension.SmallDimension + j]
                .Number;
            if (!num.HasValue)
                continue;
            if (!set.Add(num.Value))
                return false;
        }

        return true;
    }
}