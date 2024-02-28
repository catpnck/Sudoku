namespace Sudoku.Logic.Solver;

internal class Header(Node? left, Node? right) : Node(left, right, null, null, null, -1)
{
    public int Size { get; set; }
}