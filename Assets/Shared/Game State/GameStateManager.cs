using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameContext
{
    // Add các service hoặc manager khác nếu cần
    public GameManager GameManager { get; set; }
    public UIManager UIManager { get; set; }
    public MusicConductor MusicConductor;
    public ParticleEnvironmentManager ParticleEnvironmentManager ;
    public ScoreManager ScoreManager;
    public WinLoseHandleManager WinLoseHandleManager;

    public TileSpawner TileSpawner;
}

public class GameStateManager : UnitySingleton<GameStateManager>
{
    private IState currentState;
    private Dictionary<Type, IState> states = new();
    private GameContext gameContext;

    public static GameContext GameContext => Instance.gameContext;
    
    protected override void Awake()
    {
        base.Awake();

        InjectReferences();
    }

    private void Start()
    {
        Register(new GameplayState());
        Register(new MainMenuState());
        Register(new ResultState());
        
        StartCoroutine(WaitForCalculatorUI());
    }

    private void InjectReferences()
    {
        gameContext = new();
        
        gameContext.GameManager = GameManager.Instance;
        gameContext.MusicConductor = FindFirstObjectByType<MusicConductor>();
        gameContext.ParticleEnvironmentManager = FindFirstObjectByType<ParticleEnvironmentManager>();
        gameContext.ScoreManager = FindFirstObjectByType<ScoreManager>();
        gameContext.WinLoseHandleManager = FindFirstObjectByType<WinLoseHandleManager>();
        gameContext.TileSpawner = FindFirstObjectByType<TileSpawner>();

    }

    IEnumerator WaitForCalculatorUI()
    {
        yield return Yielders.GetWaitForSeconds(0.5f);
        ChangeState<MainMenuState>();
        
    }
    
    private void Register<T>(T instance) where T : BaseState
    {
        Type type = typeof(T);
        states.Add(type, instance);
        instance.Initialize(gameContext);
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
        if (states.TryGetValue(typeof(T), out var newState))
        {
            currentState?.Exit();

            currentState = newState;

            currentState.Enter();
        }
    }
}