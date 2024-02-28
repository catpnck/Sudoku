using Sudoku.Model;

namespace Sudoku.Logic.BoardModifiers;

public class BoardRowsChanger : AbstractBoardLineChanger
{
    protected override Cell GetCell(Board board, int lineNum, int i)
    {
        return board.Cells[i, lineNum];
    }
}