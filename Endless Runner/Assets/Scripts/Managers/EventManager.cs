using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public enum EventType
{
    START,
    CONTINUE,
    PAUSE,
    SETTING,
    STOP
}

public class EventManager
{
    private static readonly IDictionary<EventType, UnityEvent> dictionary =
        new Dictionary<EventType, UnityEvent>();

    public static void Subscribe(EventType eventType, UnityAction unityAction)
    {
        UnityEvent unityEvent = null;

        if(dictionary.TryGetValue(eventType, out unityEvent))
        {
            unityEvent.AddListener(unityAction);
        }
        else
        {
            unityEvent = new UnityEvent();

            unityEvent.AddListener(unityAction);

            dictionary.Add(eventType, unityEvent);
        }
    }

    public static void UnSubscribe(EventType eventType, UnityAction unityAction)
    {
        UnityEvent unityEvent = null;

        if(dictionary.TryGetValue(eventType, out unityEvent))
        {
            unityEvent.RemoveListener(unityAction);
        }
    }

    public static void Publish(EventType eventType)
    {
        UnityEvent unityEvent = null;

        if (dictionary.TryGetValue(eventType, out unityEvent))
        {
            unityEvent.Invoke();
        }
    }
}
