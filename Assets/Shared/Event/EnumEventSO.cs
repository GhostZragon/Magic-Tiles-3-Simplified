using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnumEventSO<T> : ScriptableObject where T : Enum
{
    [SerializeField]
    private T currentValue;

    private List<Action<T>> listeners = new List<Action<T>>();

    public T Value
    {
        get => currentValue;
        set
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, value))
                return;

            currentValue = value;
            InvokeEvent();
        }
    }

    private void OnEnable()
    {
        // Khởi tạo danh sách listeners mới khi ScriptableObject được enable
        listeners = new List<Action<T>>();
    }

    public void RegisterListener(Action<T> listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
            // Gọi ngay lập tức với giá trị hiện tại
            listener.Invoke(currentValue);
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
            listeners[i]?.Invoke(currentValue);
        }
    }

    public void SetValue(T newValue)
    {
        Value = newValue;
    }

    public bool IsValue(T valueToCompare)
    {
        return EqualityComparer<T>.Default.Equals(currentValue, valueToCompare);
    }
}
