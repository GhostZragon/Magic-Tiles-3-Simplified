using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : UnitySingleton<AudioManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;

    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer mainMixer;

    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    public MusicSoundAsset MusicSoundAsset;
    
    protected override void Awake()
    {
        base.Awake();
        soundBuilderPrefab = soundBuilderGameObject.GetComponent<SoundBuilder>();
        ObjectPoolManager.GetObjectPool(soundBuilderPrefab, 10);
        MusicSoundAsset = GetComponent<MusicSoundAsset>();
    }

    public void PlayMusic(SoundConfig soundConfig)
    {
        if (soundConfig == null)
        {
            Debug.Log("Sound Config is null music");
            StopMusic(0.2f);
            return;
        }

        if (musicSource.isPlaying)
        {
            StopMusic(0.2f, () =>
            {
                musicSource.volume = soundConfig.volume;
                musicSource.clip = soundConfig.AudioClip;
                musicSource.Play();
            });
            return;
        }

        musicSource.volume = soundConfig.volume;
        musicSource.clip = soundConfig.AudioClip;
        musicSource.Play();
    }

    public void StopMusic(float duration, Action callback = null)
    {
        musicSource.DOFade(0, duration).SetUpdate(true).OnComplete(() =>
        {
            musicSource.Stop();
            callback?.Invoke();
        });
    }


    [SerializeField] private GameObject soundBuilderGameObject;
    private SoundBuilder soundBuilderPrefab;

    public SoundBuilder CreateSound(SoundConfig soundConfig, bool timeAffect = true)
    {
        if (!soundConfig) return null;
        //Debug.Log("Create Sound Builder with: " + soundConfig.name);
        var soundBuilder = ObjectPoolManager.GetObject<SoundBuilder>(soundBuilderPrefab);
        soundBuilder.Init(this, soundConfig);
        soundBuilder.SetAudioMixer(sfxMixerGroup);
        return soundBuilder;
    }
}