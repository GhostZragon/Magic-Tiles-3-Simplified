using UnityEngine;

public class WinLoseHandleManager : MonoBehaviour
{
    public enum ResultState
    {
        Win,
        Lose
    }

    [SerializeField] private MusicConductor conductor;
    [SerializeField] private SoundConfig winSoundConfig;
    [SerializeField] private SoundConfig loseSoundConfig;

    private void Awake()
    {
        GameEvent<WinGameEvent>.Register(OnWinGameEventHandle);
        GameEvent<LoseGameEvent>.Register(OnLoseGameEventHandle);
    }

    private void OnDestroy()
    {
        GameEvent<WinGameEvent>.Unregister(OnWinGameEventHandle);
        GameEvent<LoseGameEvent>.Unregister(OnLoseGameEventHandle);
    }

    private void OnLoseGameEventHandle(LoseGameEvent obj)
    {
        ShowGameResult(ResultState.Lose);
    }

    private void OnWinGameEventHandle(WinGameEvent obj)
    {
        ShowGameResult(ResultState.Win);
    }

    private void ShowGameResult(ResultState resultState)
    {
        Debug.Log("Game Result : " + resultState);
        conductor.Stop();
        GameManager.Instance.Stop();

        UIManager.Instance.Get<GameplayUI>().ShowResult();
        AudioManager.Instance.CreateSound(resultState == ResultState.Win ? winSoundConfig : loseSoundConfig).Play();
    }
}