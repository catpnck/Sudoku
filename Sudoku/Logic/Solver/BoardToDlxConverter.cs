using System.Text;
using Sudoku.Model;

namespace Sudoku.Logic.Solver;

public static class BoardToDlxConverter
{
    public static string Convert(Board board)
    {
        var sb = new StringBuilder();
        for (var x = 0; x < board.Dimension.FullDimension; x++)
        for (var y = 0; y < board.Dimension.FullDimension; y++)
        {
            var cell = board.Cells[y, x];
            if (!cell.Number.HasValue)
                sb.Append('.');
            else
                sb.Append(cell.Number);
        }

        return sb.ToString();
    }
}