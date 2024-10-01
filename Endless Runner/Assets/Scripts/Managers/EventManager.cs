using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public enum EventType
{
    START,
    CONTINUE,
    PAUSE,
    STOP
}

public class EventManager
{
    private static readonly IDictionary<EventType, UnityEvent> dictionary =
        new Dictionary<EventType, UnityEvent>();

    public static void Subscribe(EventType eventType, UnityAction action)
    {
        if(dictionary.ContainsKey(eventType) == false)
        {
            dictionary.Add(eventType, new UnityEvent());
        }
        else
        {
            dictionary[eventType].AddListener(action);
        }
    }
}
