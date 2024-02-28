using Sudoku.Model;

namespace Sudoku.Logic.BoardModifiers;

public class BoardRowsGroupChanger : AbstractBoardGroupsChanger
{
    protected override Cell GetCell(Board board, int groupNum, int i, int j)
    {
        return board.Cells[j, groupNum * board.Dimension.SmallDimension + i];
    }
}