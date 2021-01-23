using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HeavyController _heavyController;

    // [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _screemerPanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private FirstPersonAIO _fpsController;

    private Vector3 playerPos = Vector3.zero;


    public enum GameState
    {
        Prepare,
        Game,
        GameOver,
        Victory
    }

    private GameState _currentState = GameState.Prepare;


    public void Awake()
    {
        _heavyController.OnGameOver += () => ChangeState(GameState.GameOver);
        ChangeState(GameState.Prepare);
        _screemerPanel.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeState(GameState.Game);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _heavyController.Reset();
            //_playerController.Reset();
            ChangeState(GameState.Game);
        }
    }

    public void ChangeState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Prepare:
                _menuPanel.SetActive(true);
                _heavyController.Reset();
                _screemerPanel.SetActive(false);
                _fpsController.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                //_playerController.Reset();
                break;
            case GameState.Game:
                _heavyController.StartGame();
                _screemerPanel.SetActive(false);
                _menuPanel.SetActive(false);
                _fpsController.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
                _fpsController.gameObject.transform.position = playerPos;
                // _playerController.StartGame();
                break;
            case GameState.GameOver:
                _screemerPanel.SetActive(true);
                StartCoroutine(GameOver());
                _fpsController.enabled = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
                // _playerController.GameOver();
                break;
            case GameState.Victory:
                // _playerController.Victory();
                _fpsController.enabled = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
                _heavyController.StopGame();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(10);
        ChangeState(GameState.Prepare);
    }

    public void StartGame()
    {
        ChangeState(GameState.Game);
    }

    public void Exit()
    {
        Application.Quit();
    }
}