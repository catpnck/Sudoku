using Microsoft.CodeAnalysis;
using Sudoku.Logic.Utils;
using Sudoku.Model;

namespace Sudoku.Logic.BoardModifiers;

public abstract class AbstractBoardLineChanger : IBoardModifier
{
    public void Modify(Board board)
    {
        var (firstNum, secondNum) = RandomUtils.NextTwoFromOneGroup(board.Dimension);

        var secondLine = new Optional<int>[board.Dimension.FullDimension];
        for (var i = 0; i < board.Dimension.FullDimension; i++)
        {
            secondLine[i] = GetCell(board, firstNum, i).Number;
            GetCell(board, firstNum, i).Number = GetCell(board, secondNum, i).Number;
        }

        for (var i = 0; i < board.Dimension.FullDimension; i++)
            GetCell(board, secondNum, i).Number = secondLine[i];
    }

    protected abstract Cell GetCell(Board board, int lineNum, int i);
}