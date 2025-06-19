using UnityEngine;

public class ButtonClickSound : BtnController
{
    [SerializeField] private SoundConfig clickSoundConfig;

    protected override void OnClickBtn()
    {
        base.OnClickBtn();
        AudioManager.Instance.CreateSound(clickSoundConfig).Play();
    }
}