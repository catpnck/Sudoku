using Sudoku.Model;

namespace Sudoku.Logic.BoardModifiers;

public class BoardColumnsChanger : AbstractBoardLineChanger
{
    protected override Cell GetCell(Board board, int lineNum, int i)
    {
        return board.Cells[lineNum, i];
    }
}