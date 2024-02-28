using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Sudoku.Model;
using Sudoku.State;

namespace Sudoku.Views.Controls;

public partial class NumberCell : UserControl
{
    private int _y;
    private int _x;
    
    public NumberCell()
    {
        InitializeComponent();
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        var gameState = GameStateHolder.GetGameState();
        if (gameState.SelectedCell == gameState.Board?.Cells[_y, _x])
        {
            gameState.SelectedCell = null;
            return;
        }
        gameState.SelectedCell = gameState.Board?.Cells[_y, _x];
    }

    public void SetCoordinates(int y, int x)
    {
        _y = y;
        _x = x;
    }
}