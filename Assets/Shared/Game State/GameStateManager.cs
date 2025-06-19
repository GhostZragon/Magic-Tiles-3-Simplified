using System;
using System.Collections.Generic;
using MacacaGames;
using UnityEngine;
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
        Register(new ResultState());
        
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
    private GameManager gameManager;
    private MusicConductor musicConductor;
    private WinLoseHandleManager winLoseHandleManager;

    public override void Initialize()
    {
        base.Initialize();
        gameManager = GameManager.Instance;
        musicConductor = gameManager.conductor;
    }

    protected override void AfterPrepareState()
    {
        base.AfterPrepareState();
        // load data into game manager and audio music
        UIManager.Instance.Show<GameplayUI>();
        gameManager.GameState = e_GameState.None;
    }

    protected override void AfterDestroyState()
    {
        base.AfterDestroyState();
        gameManager.GameState = e_GameState.None;
    }

    public override void Update()
    {
        base.Update();
        if (gameManager.GameState == e_GameState.Playing)
        {
            gameManager.UpdateHandleNoteSpawning();
        }
    }

    protected override void Register()
    {
        base.Register();
        GameEvent<EndGameEvent>.Register(OnWinGameEventHandle);
    }

    protected override void UnRegister()
    {
        base.UnRegister();
        GameEvent<EndGameEvent>.Unregister(OnWinGameEventHandle);
    }


    private void OnWinGameEventHandle(EndGameEvent obj)
    {
        musicConductor.Stop();
        gameManager.Stop();
        gameManager.GameResult = obj.ResultState;
        ChangeState<ResultState>();
        gameManager.GameState = e_GameState.Result;
    }
}

public class MainMenuState : BaseState
{
    private MainMenuUI mainMenuUI;
    public override void Initialize()
    {
        base.Initialize();
        mainMenuUI = UIManager.Instance.Get<MainMenuUI>();
        mainMenuUI.uiSongManager.InitSongList();
    }

    protected override void AfterPrepareState()
    {
        base.AfterPrepareState();
        // load data into game manager and audio music
        UIManager.Instance.Show<MainMenuUI>();
    }
}

public class ResultState : BaseState
{
    private WinLoseHandleManager winLoseHandleManager;
    
    public override void Initialize()
    {
        base.Initialize();
        winLoseHandleManager = GameObject.FindFirstObjectByType<WinLoseHandleManager>();
    }

    protected override void AfterPrepareState()
    {
        base.AfterPrepareState();
        
        winLoseHandleManager.ShowGameResult(GameManager.Instance.GameResult);
        
        UIManager.Instance.Get<GameplayUI>().ShowResult();
    }

}

