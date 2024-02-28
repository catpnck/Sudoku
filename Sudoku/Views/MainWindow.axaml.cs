using Avalonia.Controls;
using Avalonia.Input;
using Sudoku.State;
using Sudoku.Views;

namespace Sudoku;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        GameStateHolder.GetGameState().CreateNewGame();
    }
    
    protected override void OnKeyUp(KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.D1:
                SetSelectedCellNumber(1);
                break;
            case Key.D2:
                SetSelectedCellNumber(2);
                break;
            case Key.D3:
                SetSelectedCellNumber(3);
                break;
            case Key.D4:
                SetSelectedCellNumber(4);
                break;
            case Key.D5:
                SetSelectedCellNumber(5);
                break;
            case Key.D6:
                SetSelectedCellNumber(6);
                break;
            case Key.D7:
                SetSelectedCellNumber(7);
                break;
            case Key.D8:
                SetSelectedCellNumber(8);
                break;
            case Key.D9:
                SetSelectedCellNumber(9);
                break;
        }
    }

    private void SetSelectedCellNumber(int number)
    {
        var selectedCell = GameStateHolder.GetGameState().SelectedCell;
        if (selectedCell == null || selectedCell.IsInitial) 
            return;
        selectedCell.Number = number;
        if (selectedCell.Number.HasValue && selectedCell.Number.Value != selectedCell.Solution)
            GameStateHolder.GetGameState().BurnLive();
    }

    public void ResetGame()
    {
        GameStateHolder.GetGameState().CreateNewGame();
        Content = new LevelChooseView();
    }
}