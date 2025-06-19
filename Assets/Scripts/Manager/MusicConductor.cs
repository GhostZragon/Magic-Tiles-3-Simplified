using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicConductor : MonoBehaviour
{
    [Header("Music Settings")]
    public AudioClip musicClip;

    public event Action<int> OnBeat;
    private AudioSource audioSource;
    private double musicStartDspTime;
    public float secondsPerBeat;
    private int lastBeat = -1;

    public AudioSource AudioSource => audioSource;

    private BeatManager beatManager;

    void Start()
    {
        beatManager = BeatManager.Instance;
        audioSource = GetComponent<AudioSource>();
    }

    public void Setup(float bpm, float pitchMultiplier, float fallingDelay)
    {
        audioSource.clip = musicClip;

        secondsPerBeat = 60f / (bpm * pitchMultiplier);
        audioSource.pitch = pitchMultiplier;
        
        Debug.Log("Play with delay: " + fallingDelay);
        
        audioSource.PlayDelayed(fallingDelay);
        beatManager.SetBpm(bpm);
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}