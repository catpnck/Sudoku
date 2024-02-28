using Microsoft.CodeAnalysis;
using Sudoku.Logic.BoardModifiers;
using Sudoku.Model;

namespace Sudoku.Tests.Logic.BoardModifiers;

[TestFixture]
[TestOf(nameof(BoardColumnsGroupChanger))]
public class BoardColumnsGroupChangerTest
{
    [Test]
    [Repeat(100)]
    public void CorrectColumnGroupsChanging()
    {
        var board = new Board(Dimension.NINE);
        var original = new Optional<int>[board.Dimension.FullDimension, board.Dimension.FullDimension];
        for (var i = 0; i < board.Dimension.FullDimension; i++)
        for (var j = 0; j < board.Dimension.FullDimension; j++)
            original[i, j] = board.Cells[i, j].Number;

        var changer = new BoardColumnsGroupChanger();
        changer.Modify(board);

        var changedGroups = new List<int>();

        for (var i = 0; i < board.Dimension.SmallDimension; i++)
        for (var j = i; j < board.Dimension.SmallDimension; j++)
        {
            if (i == j)
                continue;
            if (!board.Cells[i * board.Dimension.SmallDimension, 0].Number
                    .Equals(original[j * board.Dimension.SmallDimension, 0])) continue;
            changedGroups.Add(i);
            changedGroups.Add(j);
        }

        Assert.That(changedGroups, Has.Count.EqualTo(2));
    }
}