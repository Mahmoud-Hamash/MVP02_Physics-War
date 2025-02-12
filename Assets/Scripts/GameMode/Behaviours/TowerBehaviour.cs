using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    private Tower _tower;
    private bool _notified = false;
    private void Start()
    {
        _tower = GetComponent<Tower>();
    }

    private void Update()
    {
        if (_tower.isImpacted && !_notified)
        {
            _notified = true;
            GameEventManager.TriggerEvent(GameEventType.TowerCollapsed);
        }
    }
}
