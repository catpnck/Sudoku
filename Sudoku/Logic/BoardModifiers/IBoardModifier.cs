using Sudoku.Model;

namespace Sudoku.Logic.BoardModifiers;

public interface IBoardModifier
{
    public void Modify(Board board);
}