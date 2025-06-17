using System;
using System.Collections.Generic;
using MacacaGames;
using UnityEngine.SceneManagement;


public class GameStateManager : Singleton<GameStateManager>
{
    private IState currentState;
    private Dictionary<Type, IState> states = new();

    public GameStateManager()
    {
        
    }

    private void Register<T>(T instance) where T : BaseState
    {
        Type type = typeof(T);
        states.Add(type, instance);
        instance.Initialize();
    }

    public T Get<T>() where T : IState
    {
        Type type = typeof(T);

        if (states.TryGetValue(type, out var state))
        {
            return (T)state;
        }
        
        return default(T);
    }

    
    
    public void ChangeState<T>() where T : BaseState
    {
        if(states.TryGetValue(typeof(T),out var newState))
        {
            currentState?.Exit();

            currentState = newState;

            currentState.Enter();
        }
    }
}
