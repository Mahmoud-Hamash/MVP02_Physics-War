using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSpawner : MonoBehaviour
{
    [SerializeField] private GameOverBehaviour _gameOverPrefab;

    private void Start()
    {
        GameEventManager.StartListening(GameEventType.TowerCollapsed, OnTowerCollapsed);
        GameEventManager.StartListening(GameEventType.PalisadeWrecked, OnPalisadeWrecked);
    }

    private void OnTowerCollapsed(System.Object obj)
    {
        Invoke(nameof(GameWon), 2f);
    }

    private void GameWon()
    {
        GameOverBehaviour _gameOver = Instantiate(_gameOverPrefab);
        _gameOver.SetGameOverText("You\nWon!");
        _gameOver.Zoom();
    }

    private void OnPalisadeWrecked(System.Object obj)
    {
        Invoke(nameof(GameLost), 2f);
    }

    private void GameLost()
    {
        GameOverBehaviour _gameOver = Instantiate(_gameOverPrefab);
        _gameOver.SetGameOverText("Game\nOver");
        _gameOver.Zoom();
    }
}
