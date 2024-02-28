using Microsoft.CodeAnalysis;
using Sudoku.Logic.BoardModifiers;
using Sudoku.Model;

namespace Sudoku.Tests.Logic.BoardModifiers;

[TestFixture]
[TestOf(nameof(BoardColumnsChanger))]
public class BoardColumnsChangerTest
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

        var changedCols = new List<int>();

        var rowsChanger = new BoardColumnsChanger();
        rowsChanger.Modify(board);

        for (var i = 0; i < board.Dimension.FullDimension; i++)
            if (!board.Cells[i, 0].Number.Equals(original[i, 0]))
                changedCols.Add(i);

        Assert.That(changedCols, Has.Count.EqualTo(2));
        Assert.That(changedCols[0] / board.Dimension.SmallDimension,
            Is.EqualTo(changedCols[1] / board.Dimension.SmallDimension));

        for (var i = 0; i < board.Dimension.FullDimension; i++)
        {
            Assert.That(original[changedCols[0], i], Is.EqualTo(board.Cells[changedCols[1], i].Number));
            Assert.That(original[changedCols[1], i], Is.EqualTo(board.Cells[changedCols[0], i].Number));
        }
    }
}