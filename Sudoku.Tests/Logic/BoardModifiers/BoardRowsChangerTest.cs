using Microsoft.CodeAnalysis;
using Sudoku.Logic.BoardModifiers;
using Sudoku.Model;

namespace Sudoku.Tests.Logic.BoardModifiers;

[TestFixture]
[TestOf(nameof(BoardRowsChanger))]
public class BoardRowsChangerTest
{
    [Test]
    [Repeat(100)]
    public void CorrectRowsChanging()
    {
        var board = new Board(Dimension.NINE);
        var original = new Optional<int>[board.Dimension.FullDimension, board.Dimension.FullDimension];
        for (var i = 0; i < board.Dimension.FullDimension; i++)
        for (var j = 0; j < board.Dimension.FullDimension; j++)
            original[i, j] = board.Cells[i, j].Number;

        var changedRows = new List<int>();

        var rowsChanger = new BoardRowsChanger();
        rowsChanger.Modify(board);

        for (var i = 0; i < board.Dimension.FullDimension; i++)
            if (!board.Cells[0, i].Number.Equals(original[0, i]))
                changedRows.Add(i);

        Assert.That(changedRows, Has.Count.EqualTo(2));
        Assert.That(changedRows[0] / board.Dimension.SmallDimension,
            Is.EqualTo(changedRows[1] / board.Dimension.SmallDimension));

        for (var i = 0; i < board.Dimension.FullDimension; i++)
        {
            Assert.That(original[i, changedRows[0]], Is.EqualTo(board.Cells[i, changedRows[1]].Number));
            Assert.That(original[i, changedRows[1]], Is.EqualTo(board.Cells[i, changedRows[0]].Number));
        }
    }
}