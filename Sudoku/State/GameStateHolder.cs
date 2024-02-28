namespace Sudoku.State;

public static class GameStateHolder
{
    private static GameState? _gameState;

    public static GameState GetGameState()
    {
        return _gameState ??= new GameState();
    }
}