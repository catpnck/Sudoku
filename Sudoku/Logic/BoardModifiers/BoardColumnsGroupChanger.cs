using Sudoku.Model;

namespace Sudoku.Logic.BoardModifiers;

public class BoardColumnsGroupChanger : AbstractBoardGroupsChanger
{
    protected override Cell GetCell(Board board, int groupNum, int i, int j)
    {
        return board.Cells[groupNum * board.Dimension.SmallDimension + i, j];
    }
}