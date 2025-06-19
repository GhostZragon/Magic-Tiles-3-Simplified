using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameContext
{
    public GameManager GameManager { get; set; }
    public UIManager UIManager { get; set; }
    public MusicConductor MusicConductor;
    public ParticleEnvironmentManager ParticleEnvironmentManager ;
    public ScoreManager ScoreManager;
    public WinLoseHandleManager WinLoseHandleManager;

    public TileSpawner TileSpawner;
    // Add các service hoặc manager khác nếu cần
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

public class GameplayState : BaseState
{
    private const int DEFAULT_LINE_COUNT = 4;
    private GameManager gameManager;
    private MusicConductor musicConductor;
    private ParticleEnvironmentManager particleEnvironmentManager;
    private ScoreManager scoreManager;

    private StartGameSetup startGameSetup;
    

    public override void Initialize(GameContext GameContext)
    {
        base.Initialize(GameContext);
        
        
        gameManager = GameContext.GameManager;
        particleEnvironmentManager = GameContext.ParticleEnvironmentManager;
        musicConductor = GameContext.MusicConductor;
        scoreManager = GameContext.ScoreManager;

        startGameSetup = gameManager.GetComponent<StartGameSetup>();
    }

    protected override void AfterPrepareState()
    {
        base.AfterPrepareState();
        // Setup in here
        scoreManager.ResetScoreAndCombo();
        UIManager.Instance.Show<GameplayUI>();
        gameManager.GameState = e_GameState.None;
        
        startGameSetup.Init(DEFAULT_LINE_COUNT);
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


    private void OnWinGameEventHandle(EndGameEvent eventData)
    {
        if (gameManager.GameState == e_GameState.Playing)
        {
            particleEnvironmentManager.SetActiveState(false);
            GameStateManager.Instance.StartCoroutine(Delay(eventData));
        }
    }

    private IEnumerator Delay(EndGameEvent eventData)
    {
        yield return Yielders.GetWaitForSeconds(0.5f);
        
        GameContext.TileSpawner.ClearItemInPool();
        
        musicConductor.Stop();
        
        gameManager.GameState = e_GameState.Result;
        gameManager.GameResult = eventData.ResultState;
        
        ChangeState<ResultState>();
    }
}

public class MainMenuState : BaseState
{
    private MainMenuUI mainMenuUI;

    public override void Initialize(GameContext GameContext)
    {
        base.Initialize(GameContext);
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

    public override void Initialize(GameContext GameContext)
    {
        base.Initialize(GameContext);
        winLoseHandleManager = GameContext.WinLoseHandleManager;
    }

    protected override void AfterPrepareState()
    {
        base.AfterPrepareState();

        winLoseHandleManager.ShowGameResult(GameManager.Instance.GameResult);

        UIManager.Instance.Get<GameplayUI>().ShowResult();
    }
}