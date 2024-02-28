using Microsoft.CodeAnalysis;
using Sudoku.Logic.BoardModifiers;
using Sudoku.Model;

namespace Sudoku.Tests.Logic.BoardModifiers;

[TestFixture]
[TestOf(nameof(BoardTransposer))]
public class BoardTransposerTest
{
    [Test]
    public void CorrectTransposing()
    {
        var board = new Board(Dimension.NINE);
        var original = new Optional<int>[board.Dimension.FullDimension, board.Dimension.FullDimension];
        for (var i = 0; i < board.Dimension.FullDimension; i++)
        for (var j = 0; j < board.Dimension.FullDimension; j++)
            original[i, j] = board.Cells[i, j].Number;

        var transposer = new BoardTransposer();
        transposer.Modify(board);

        for (var i = 0; i < board.Dimension.FullDimension; i++)
        for (var j = 0; j < board.Dimension.FullDimension; j++)
            Assert.That(original[i, j], Is.EqualTo(board.Cells[j, i].Number));
    }
}