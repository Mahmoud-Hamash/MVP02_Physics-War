using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _score = 0;

    private void Start()
    {
        GameEventManager.StartListening(GameEventType.SpartanHit, OnSpartanHit);
        GameEventManager.StartListening(GameEventType.TowerCollapsed, OnTowerCollapsed);
    }

    private void OnTowerCollapsed(System.Object obj)
    {
        AddScore(100);
    }

    private void OnSpartanHit(System.Object obj)
    {
        AddScore(10);
    }

    private void AddScore(int points)
    {
        _score += points;
        _scoreText.text = _score.ToString("D3");
    }
}
