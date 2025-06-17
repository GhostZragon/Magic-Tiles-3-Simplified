using System;

public class GameEvent<T> where T : IGameEvent
{
    public static event Action<T> Listeners;

    public static void Raise(T gameEvent)
    {
        Listeners?.Invoke(gameEvent);
    }

    public static void Register(Action<T> callback) => Listeners += callback;
    public static void Unregister(Action<T> callback) => Listeners -= callback;
}

