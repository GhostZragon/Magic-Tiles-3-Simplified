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

    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void Setup(float bpm, float pitchMultiplier, float fallingDelay)
    {
        audioSource.clip = musicClip;
        
        secondsPerBeat = 60f / (bpm * pitchMultiplier);
        musicStartDspTime = AudioSettings.dspTime + fallingDelay;
        audioSource.pitch = pitchMultiplier;
        audioSource.PlayScheduled(musicStartDspTime);
        
    }
    
}