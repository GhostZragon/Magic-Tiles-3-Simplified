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