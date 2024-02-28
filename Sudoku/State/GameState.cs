using System;
using System.Linq;
using Sudoku.Logic;
using Sudoku.Logic.Generator;
using Sudoku.Model;

namespace Sudoku.State;

public class GameState
{
    public Board? Board { get; private set; }

    public Level? Level { get; private set; }

    public Cell? SelectedCell { get; set; }
    
    public int Lives { get; private set; }

    public GameState()
    {
    }

    public void GenerateLevel(Level level)
    {
        Level = level;
        var generator = new PuzzleGenerator();
        Board = generator.GeneratePuzzle(level);
        Lives = level switch
        {
            Logic.Level.Easy => 5,
            Logic.Level.Medium => 4,
            Logic.Level.Hard => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
        };
    }

    public void CreateNewGame()
    {
        Board = null;
        Level = null;
        SelectedCell = null;
    }

    public void BurnLive()
    {
        Lives--;
    }

    public bool IsLivesEmpty()
    {
        return Lives == 0;
    }

    public bool IsPuzzleSolved()
    {
        return Board != null && Board.Cells.Cast<Cell>().All(c => c.Number.HasValue && c.Number.Value == c.Solution);
    }
}