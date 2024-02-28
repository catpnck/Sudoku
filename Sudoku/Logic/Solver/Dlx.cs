using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Logic.Solver;

public class Dlx(int rowCapacity, int columnCapacity)
{
    private readonly List<Header?> _columns = new(columnCapacity);
    private readonly Header? _root = new(null, null) { Size = int.MaxValue };
    private readonly List<Node?> _rows = new(rowCapacity);
    private readonly Stack<Node?> _solutionNodes = new();
    private int _initial;

    public void AddHeader()
    {
        var h = new Header(_root?.Left, _root);
        h.AttachLeftRight();
        _columns.Add(h);
    }

    public void AddRow(params int[]? newRow)
    {
        Node? first = null;
        if (newRow != null)
            foreach (var item in newRow)
            {
                if (item < 0) continue;
                if (first == null) first = AddNode(_rows.Count, item);
                else AddNode(first, item);
            }

        _rows.Add(first);
    }

    private Node AddNode(int row, int column)
    {
        var n = new Node(null, null, _columns[column]?.Up, _columns[column], _columns[column], row);
        n.AttachUpDown();
        if (n.Head != null) n.Head.Size++;
        return n;
    }

    private void AddNode(Node? firstNode, int column)
    {
        if (firstNode == null) return;
        var n = new Node(firstNode.Left, firstNode, _columns[column]?.Up, _columns[column], _columns[column],
            firstNode.Row);
        n.AttachLeftRight();
        n.AttachUpDown();
        if (n.Head != null) n.Head.Size++;
    }

    public void Give(int row)
    {
        _solutionNodes.Push(_rows[row]);
        CoverMatrix(_rows[row]);
        _initial++;
    }

    public IEnumerable<int[]> Solutions()
    {
        try
        {
            var node = ChooseSmallestColumn()?.Down;
            do
            {
                if (node == node?.Head)
                {
                    if (node == _root) yield return _solutionNodes.Select(n => n!.Row).ToArray();
                    if (_solutionNodes.Count <= _initial) continue;
                    node = _solutionNodes.Pop();
                    UncoverMatrix(node);
                    node = node?.Down;
                }
                else
                {
                    _solutionNodes.Push(node);
                    CoverMatrix(node);
                    node = ChooseSmallestColumn()?.Down;
                }
            } while (_solutionNodes.Count > _initial || node != node?.Head);
        }
        finally
        {
            Restore();
        }
    }

    private void Restore()
    {
        while (_solutionNodes.Count > 0) UncoverMatrix(_solutionNodes.Pop());
        _initial = 0;
    }

    private Header? ChooseSmallestColumn()
    {
        var traveller = _root;
        var choice = _root;
        do
        {
            traveller = (Header)traveller?.Right!;
            if (choice != null && traveller.Size < choice.Size) choice = traveller;
        } while (choice != null && traveller != _root && choice.Size > 0);

        return choice;
    }

    private void CoverRow(Node? row)
    {
        var traveller = row?.Right;
        while (traveller != row)
        {
            traveller?.DetachUpDown();
            if (traveller == null) continue;
            if (traveller.Head != null) traveller.Head.Size--;
            traveller = traveller.Right;
        }
    }

    private void UncoverRow(Node? row)
    {
        var traveller = row?.Left;
        while (traveller != row)
        {
            traveller?.AttachUpDown();
            if (traveller == null) continue;
            if (traveller.Head != null) traveller.Head.Size++;
            traveller = traveller.Left;
        }
    }

    private void CoverColumn(Node? column)
    {
        column?.DetachLeftRight();
        var traveller = column?.Down;
        while (traveller != column)
        {
            CoverRow(traveller);
            traveller = traveller?.Down;
        }
    }

    private void UncoverColumn(Node? column)
    {
        var traveller = column?.Up;
        while (traveller != column)
        {
            UncoverRow(traveller);
            traveller = traveller?.Up;
        }

        column?.AttachLeftRight();
    }

    private void CoverMatrix(Node? node)
    {
        var traveller = node;
        do
        {
            CoverColumn(traveller?.Head);
            traveller = traveller?.Right;
        } while (traveller != node);
    }

    private void UncoverMatrix(Node? node)
    {
        var traveller = node;
        do
        {
            traveller = traveller?.Left;
            UncoverColumn(traveller?.Head);
        } while (traveller != node);
    }
}