using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrimitiveEventSO<T> : ScriptableObject
{
    [SerializeField]
    protected T value;

    private List<Action<T>> listeners = new List<Action<T>>();

    public T Value
    {
        get => value;
        set 
        {
            if (EqualityComparer<T>.Default.Equals(this.value, value))
                return;
                
            this.value = value;
            InvokeEvent();
        }
    }

    private void OnEnable()
    {
        listeners = new List<Action<T>>();
    }


    public void RegisterListener(Action<T> listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
            listener.Invoke(value);
        }
    }

   
    public void UnregisterListener(Action<T> listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }

    public void InvokeEvent()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i]?.Invoke(value);
        }
    }

    public void SetValue(T newValue)
    {
        Value = newValue;
    }
}