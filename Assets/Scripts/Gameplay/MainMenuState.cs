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