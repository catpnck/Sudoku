using Microsoft.CodeAnalysis;
using Sudoku.Logic.Utils;
using Sudoku.Model;

namespace Sudoku.Logic.BoardModifiers;

public abstract class AbstractBoardGroupsChanger : IBoardModifier
{
    public void Modify(Board board)
    {
        var (firstGroupNum, secondGroupNum) = RandomUtils.NextTwo(board.Dimension);

        var firstGroup = new Optional<int>[board.Dimension.SmallDimension, board.Dimension.FullDimension];
        for (var i = 0; i < board.Dimension.SmallDimension; i++)
        for (var j = 0; j < board.Dimension.FullDimension; j++)
            firstGroup[i, j] = GetCell(board, firstGroupNum, i, j).Number;

        for (var i = 0; i < board.Dimension.SmallDimension; i++)
        for (var j = 0; j < board.Dimension.FullDimension; j++)
        {
            GetCell(board, firstGroupNum, i, j).Number = GetCell(board, secondGroupNum, i, j).Number;
            GetCell(board, secondGroupNum, i, j).Number = firstGroup[i, j];
        }
    }

    protected abstract Cell GetCell(Board board, int groupNum, int i, int j);
}