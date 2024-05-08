using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventListener<T> : MonoBehaviour
{
    [SerializeField]
    EventChannel<T> eventChannel;
    [SerializeField]
    UnityEvent<T> unityEvent;

    public void Raise(T value) => unityEvent?.Invoke(value);

    private void Awake() => eventChannel.Register(this);

    private void OnDestroy() => eventChannel.Unregister(this);
}