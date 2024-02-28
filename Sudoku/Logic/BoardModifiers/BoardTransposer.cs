using Microsoft.CodeAnalysis;
using Sudoku.Model;

namespace Sudoku.Logic.BoardModifiers;

public class BoardTransposer : IBoardModifier
{
    public void Modify(Board board)
    {
        var result = new Optional<int>[board.Dimension.FullDimension, board.Dimension.FullDimension];
        for (var i = 0; i < board.Dimension.FullDimension; i++)
        for (var j = 0; j < board.Dimension.FullDimension; j++)
            result[j, i] = board.Cells[i, j].Number;

        for (var i = 0; i < board.Dimension.FullDimension; i++)
        for (var j = 0; j < board.Dimension.FullDimension; j++)
            board.Cells[i, j].Number = result[i, j];
    }
}