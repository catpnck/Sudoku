using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Logic.Solver;

public class PuzzleSolver
{
    private Dlx? _dlx;

    private void Build()
    {
        const int rows = 9 * 9 * 9, columns = 4 * 9 * 9;
        _dlx = new Dlx(rows, columns);
        for (var i = 0; i < columns; i++) _dlx.AddHeader();

        for (int cell = 0, row = 0; row < 9; row++)
        for (var column = 0; column < 9; column++)
        {
            var box = row / 3 * 3 + column / 3;
            for (var digit = 0; digit < 9; digit++)
                _dlx.AddRow(cell, 81 + row * 9 + digit, 2 * 81 + column * 9 + digit, 3 * 81 + box * 9 + digit);
            cell++;
        }
    }

    public IEnumerable<string> Solutions(string puzzle)
    {
        ArgumentNullException.ThrowIfNull(puzzle);
        if (puzzle.Length != 81) throw new ArgumentException("The input is not of the correct length.");
        if (_dlx == null) Build();

        for (var i = 0; i < puzzle.Length; i++)
        {
            if (puzzle[i] == '0' || puzzle[i] == '.') continue;
            if (puzzle[i] < '1' && puzzle[i] > '9')
                throw new ArgumentException($"Input contains an invalid character: ({puzzle[i]})");
            var digit = puzzle[i] - '0' - 1;
            _dlx?.Give(i * 9 + digit);
        }

        return Iterator();

        IEnumerable<string> Iterator()
        {
            var sb = new StringBuilder(new string('.', 81));
            foreach (var rows in _dlx?.Solutions()!)
            {
                foreach (var r in rows) sb[r / 81 * 9 + r / 9 % 9] = (char)(r % 9 + '1');
                yield return sb.ToString();
            }
        }
    }
}