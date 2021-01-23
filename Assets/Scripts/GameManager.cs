using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HeavyController _heavyController;
    [SerializeField] private PlayerController _playerController;


    public enum GameState
    {
        Prepare,
        Game,
        GameOver,
        Victory
    }

    private GameState _currentState = GameState.Prepare;

    public void ChangeState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Prepare:
                _heavyController.Reset();
                _playerController.Reset();
                break;
            case GameState.Game:
                _heavyController.StartGame();
                _playerController.StartGame();
                break;
            case GameState.GameOver:
                _playerController.GameOver();
                break;
            case GameState.Victory:
                _playerController.Victory();
                _heavyController.StopGame();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}