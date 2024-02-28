using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using Sudoku.Logic;
using Sudoku.Model;
using Sudoku.State;
using Sudoku.Views.Controls;

namespace Sudoku.Views;

public partial class GameView : UserControl
{
    private BigCell[,] _bigCells;

    private DispatcherTimer _dispatcherTimer;
    private DateTime _startTime;

    public GameView(Level level)
    {
        InitializeComponent();
        _startTime = DateTime.Now;
        WarningTextBlock.Text = "";

        GameStateHolder.GetGameState().GenerateLevel(level);
        var board = GameStateHolder.GetGameState().Board;
        if (board == null)
        {
            WarningTextBlock.Text = "Произошла неожиданная ошибка";
            return;
        }

        FillBigCellsArray(board.Dimension);

        var validator = new BoardValidator();
        if (!validator.IsValid(board))
        {
            WarningTextBlock.Text = "Не удалось загрузить задачу";
            return;
        }

        _dispatcherTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(30)
        };
        _dispatcherTimer.Tick += DrawBoard;
        _dispatcherTimer.Start();
    }

    private void FillBigCellsArray(Dimension dimension)
    {
        _bigCells = new BigCell[dimension.SmallDimension, dimension.SmallDimension];
        _bigCells[0, 0] = BigCell00;
        _bigCells[1, 0] = BigCell10;
        _bigCells[2, 0] = BigCell20;
        _bigCells[0, 1] = BigCell01;
        _bigCells[1, 1] = BigCell11;
        _bigCells[2, 1] = BigCell21;
        _bigCells[0, 2] = BigCell02;
        _bigCells[1, 2] = BigCell12;
        _bigCells[2, 2] = BigCell22;

        for (var y = 0; y < _bigCells.GetLength(0); y++)
        for (var x = 0; x < _bigCells.GetLength(1); x++)
        for (var i = 0; i < _bigCells.GetLength(0); i++)
        for (var j = 0; j < _bigCells.GetLength(0); j++)
            _bigCells[y, x].NumberCells[i, j]
                .SetCoordinates(y * dimension.SmallDimension + i, x * dimension.SmallDimension + j);
    }

    private void DrawBoard(object? sender, EventArgs e)
    {
        var gameState = GameStateHolder.GetGameState();
        var board = gameState.Board;
        if (board == null)
            return;

        for (var i = 0; i < _bigCells.GetLength(0); i++)
        for (var j = 0; j < _bigCells.GetLength(1); j++)
        {
            var bigCell = _bigCells[i, j];
            for (var k = 0; k < board.Dimension.SmallDimension; k++)
            for (var l = 0; l < board.Dimension.SmallDimension; l++)
            {
                var numberTextBlock = bigCell.NumberCells[k, l].NumberTextBlock;
                var cell = board.Cells[i * board.Dimension.SmallDimension + k, j * board.Dimension.SmallDimension + l];
                PrintNumberInCell(cell, numberTextBlock);
            }
        }

        HighlightSelectedCell(gameState, board.Dimension);
        CheckAndDrawLives();
        DrawTimer();
        GameOverIfNeed();
    }

    private void CheckAndDrawLives()
    {
        if (GameStateHolder.GetGameState().Lives == 0)
        {
            GameOverIfNeed();
            return;
        }

        var livesStr = "";
        for (var i = 0; i < GameStateHolder.GetGameState().Lives; i++)
            livesStr += '\u2764';
        WarningTextBlock.Text = livesStr;
    }

    private void DrawTimer()
    {
        var elapsed = DateTime.Now - _startTime;
        ClockTextBlock.Text = elapsed.ToString("mm\\:ss");
    }

    private void GameOverIfNeed()
    {
        if (!GameStateHolder.GetGameState().IsPuzzleSolved() && !GameStateHolder.GetGameState().IsLivesEmpty())
            return;

        if (!GameStateHolder.GetGameState().IsPuzzleSolved())
            WarningTextBlock.Text = "You win!";
        if (GameStateHolder.GetGameState().IsLivesEmpty())
            WarningTextBlock.Text = "GameOver :(";

        NewGameButton.IsVisible = true;
        _dispatcherTimer.Stop();
    }

    private void PrintNumberInCell(Cell cell, TextBlock numberTextBlock)
    {
        numberTextBlock.Text = cell.Number.HasValue ? cell.Number.Value.ToString() : "";
        if (cell.IsInitial)
            numberTextBlock.Foreground = Brushes.Black;
        else if (cell.Number.HasValue && cell.Solution == cell.Number.Value)
            numberTextBlock.Foreground = Brushes.Blue;
        else if (cell.Number.HasValue)
            numberTextBlock.Foreground = Brushes.Red;
    }

    private void HighlightSelectedCell(GameState gameState, Dimension dimension)
    {
        foreach (var bigCell in _bigCells)
        {
            foreach (var numberCell in bigCell.NumberCells)
            {
                numberCell.Background = Brushes.White;
            }
        }

        if (gameState.SelectedCell == null)
            return;

        var y = gameState.SelectedCell.Y;
        var x = gameState.SelectedCell.X;
        _bigCells[y / dimension.SmallDimension, x / dimension.SmallDimension]
            .NumberCells[y % dimension.SmallDimension, x % dimension.SmallDimension].Background = Brushes.Aquamarine;
    }

    private void NewGameButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ((MainWindow)Parent!).ResetGame();
    }
}