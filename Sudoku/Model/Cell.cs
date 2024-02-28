using System;
using Microsoft.CodeAnalysis;

namespace Sudoku.Model;

public class Cell(int y, int x)
{
    public int Y { get; } = y;
    public int X { get; } = x;

    private Optional<int> _number;

    public Optional<int> Number
    {
        get => _number;
        set => _number = value is { HasValue: true, Value: >= 0 and <= 9 } ? value : new Optional<int>();
    }

    public int Solution { get; set; }

    public bool IsInitial { get; set; } = true;

    public bool IsValid { get; set; } = true;

    public override string ToString()
    {
        return _number.ToString();
    }
}