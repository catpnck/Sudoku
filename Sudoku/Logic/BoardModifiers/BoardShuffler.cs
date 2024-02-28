using Sudoku.Logic.Utils;
using Sudoku.Model;

namespace Sudoku.Logic.BoardModifiers;

public class BoardShuffler
{
    private const int Attempts = 100;

    private readonly IBoardModifier[] _modifiers =
    [
        new BoardTransposer(),
        new BoardRowsChanger(),
        new BoardColumnsChanger(),
        new BoardColumnsGroupChanger(),
        new BoardRowsGroupChanger()
    ];

    public void ShuffleBoard(Board board)
    {
        for (var i = 0; i < Attempts; i++)
            RandomUtils.Choose(_modifiers).Modify(board);
    }
}