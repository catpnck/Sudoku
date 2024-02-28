namespace Sudoku.Logic.Solver;

internal class Node
{
    public Node(Node? left, Node? right, Node? up, Node? down, Header? head, int row)
    {
        Left = left ?? this;
        Right = right ?? this;
        Up = up ?? this;
        Down = down ?? this;
        Head = head ?? this as Header;
        Row = row;
    }

    public Node? Left { get; private set; }
    public Node? Right { get; private set; }
    public Node? Up { get; private set; }
    public Node? Down { get; private set; }
    public Header? Head { get; }
    public int Row { get; }

    public void AttachLeftRight()
    {
        if (Left != null) Left.Right = this;
        if (Right != null) Right.Left = this;
    }

    public void AttachUpDown()
    {
        if (Up != null) Up.Down = this;
        if (Down != null) Down.Up = this;
    }

    public void DetachLeftRight()
    {
        if (Left == null) return;
        Left.Right = Right;
        if (Right != null) Right.Left = Left;
    }

    public void DetachUpDown()
    {
        if (Up == null) return;
        Up.Down = Down;
        if (Down != null) Down.Up = Up;
    }
}