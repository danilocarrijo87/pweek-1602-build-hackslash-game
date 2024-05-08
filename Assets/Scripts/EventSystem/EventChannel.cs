using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class EventChannel<T> : ScriptableObject
{
    readonly HashSet<EventListener<T>> observers = new();

    public void Invoke(T value)
    {
        foreach (var observer in observers)
        {
            observer.Raise(value);
        }
    }

    public void Register(EventListener<T> listener) => observers.Add(listener);

    public void Unregister(EventListener<T> listener) => observers.Remove(listener);
}