using UnityEngine;
using System;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;

    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EventManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("EventManager");
                    _instance = obj.AddComponent<EventManager>();
                }
            }
            return _instance;
        }
    }

    private Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    public void StartListening(string eventName, Action listener)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent += listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(string eventName, Action listener)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent -= listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public void TriggerEvent(string eventName)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}