using System.Collections;

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