using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    private TowerMed _tower;
    private bool _notified = false;
    private void Start()
    {
        _tower = GetComponent<TowerMed>();
    }

    private void Update()
    {
        if (_tower.IsDestroyed() && !_notified)
        {
            _notified = true;
            GameEventManager.TriggerEvent(GameEventType.TowerCollapsed);
        }
    }
}
