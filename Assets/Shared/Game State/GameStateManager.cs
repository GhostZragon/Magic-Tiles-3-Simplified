using System;
using System.Collections.Generic;
using MacacaGames;
using UnityEngine.SceneManagement;


public class GameStateManager : UnitySingleton<GameStateManager>
{
    private IState currentState;
    private Dictionary<Type, IState> states = new();

    protected override void Awake()
    {
        base.Awake();
        Register(new GameplayState());
        Register(new MainMenuState());
        
        ChangeState<MainMenuState>();
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

    private void Update()
    {
        currentState?.Update();
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

public class GameplayState : BaseState
{
    protected override void AfterPrepareState()
    {
        base.AfterPrepareState();
        // load data into game manager and audio music
        UIManager.Instance.Show<GameplayUI>();
    }
}

public class MainMenuState : BaseState
{
    protected override void AfterPrepareState()
    {
        base.AfterPrepareState();
        // load data into game manager and audio music
        UIManager.Instance.Show<MainMenuUI>();
    }
}