using UnityEngine;
using System.Collections.Generic;
using System;

public enum GameEventType
{
    PalisadeWrecked,
    SpartanDied,
    SpartanHit,
    TowerCollapsed
}

public class GameEventManager : MonoBehaviour
{
    private static GameEventManager _eventManager;

    private Dictionary<GameEventType, Action<System.Object>> _listeners;
    
    private static GameEventManager Instance
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = FindFirstObjectByType(typeof(GameEventManager)) as GameEventManager;
                if (!_eventManager)
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                else
                    _eventManager.Init();
            }
            return _eventManager;
        }
    }

    void Init()
    {
        if (_listeners == null)
        {
            _listeners = new Dictionary<GameEventType, Action<System.Object>>();
        }
    }

    public static void StartListening(GameEventType eventType, Action<System.Object> listener)
    {
        if (!Instance._listeners.TryAdd(eventType, listener))
        {
            Instance._listeners[eventType] += listener;
        }
    }

    public static void StopListening(GameEventType eventType, Action<System.Object> listener)
    {
        if (Instance._listeners.ContainsKey(eventType))
        {
            Instance._listeners[eventType] -= listener;
        }
    }
    
    public static void TriggerEvent(GameEventType eventType, System.Object eventParam = null)
    {
        if (Instance._listeners.TryGetValue(eventType, out Action<System.Object> action))
        {
            action.Invoke(eventParam);
        }
    }
}