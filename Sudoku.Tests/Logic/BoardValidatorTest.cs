using Sudoku.Logic;
using Sudoku.Model;

namespace Sudoku.Tests.Logic;

[TestFixture]
[TestOf(nameof(BoardValidator))]
public class BoardValidatorTest
{
    [Test]
    public void WrongRow()
    {
        var board = new Board(Dimension.NINE);
        (board.Cells[0, 0], board.Cells[0, 1]) = (board.Cells[0, 1], board.Cells[0, 0]);

        var validator = new BoardValidator();
        var result = validator.IsValid(board);
        Assert.That(result, Is.False);
    }

    [Test]
    public void WrongColumn()
    {
        var board = new Board(Dimension.NINE);
        (board.Cells[0, 0], board.Cells[1, 0]) = (board.Cells[1, 0], board.Cells[0, 0]);

        var validator = new BoardValidator();
        var result = validator.IsValid(board);
        Assert.That(result, Is.False);
    }
}