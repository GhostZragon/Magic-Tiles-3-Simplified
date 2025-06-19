using UnityEngine;

public class WinLoseHandleManager : MonoBehaviour
{
   

    [SerializeField] private MusicConductor conductor;
    [SerializeField] private SoundConfig winSoundConfig;
    [SerializeField] private SoundConfig loseSoundConfig;
    

    public void ShowGameResult(e_ResultState eResultState)
    {
        
        Debug.Log("Game Result : " + eResultState);
 

        UIManager.Instance.Get<GameplayUI>().ShowResult();
        AudioManager.Instance.CreateSound(eResultState == e_ResultState.Win ? winSoundConfig : loseSoundConfig).Play();
    }
} public enum e_ResultState
{
    Win,
    Lose
}