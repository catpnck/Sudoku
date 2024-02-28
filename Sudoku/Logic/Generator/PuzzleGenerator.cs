using System.Linq;
using Microsoft.CodeAnalysis;
using Sudoku.Logic.BoardModifiers;
using Sudoku.Logic.Solver;
using Sudoku.Logic.Utils;
using Sudoku.Model;

namespace Sudoku.Logic.Generator;

public class PuzzleGenerator
{
    public Board GeneratePuzzle(Level level)
    {
        var board = new Board(Dimension.NINE);
        var boardShuffler = new BoardShuffler();
        boardShuffler.ShuffleBoard(board);

        var solver = new PuzzleSolver();
        for (var i = 0; i < (int)level; i++)
        {
            var cell = RandomUtils.ChooseCell(board);
            if (!cell.Number.HasValue)
                continue;
            var number = cell.Number.Value;
            cell.Number = new Optional<int>();
            cell.IsInitial = false;
            var solutions = solver.Solutions(BoardToDlxConverter.Convert(board));
            if (solutions.Count() <= 1) 
                continue;
            cell.Number = number;
            cell.IsInitial = true;
        }

        var solution = solver.Solutions(BoardToDlxConverter.Convert(board)).First();
        FillSolution(board, solution);

        return board;
    }

    private void FillSolution(Board board, string solution)
    {
        var (y, x) = (0, 0);
        foreach (var number in solution.Select(chr => chr - '0'))
        {
            board.Cells[y, x].Solution = number;
            y++;
            if (y != board.Dimension.FullDimension) continue;
            y = 0;
            x++;
        }
    }
}