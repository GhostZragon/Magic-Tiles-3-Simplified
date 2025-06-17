using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicConductor : MonoBehaviour
{
    [Header("Music Settings")]
    public AudioClip musicClip;

    private float bpm = 140f;

    public event Action<int> OnBeat; // Optional: callback khi tới 1 beat

    private AudioSource audioSource;
    private double musicStartDspTime;
    private float secondsPerBeat;
    private int lastBeat = -1;
    private bool hasStarted = false;
    private float remainingTime;

    private double scheduledEndTime;

    public bool HasEnded = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void Setup(float f, float backgroundMusicDelay)
    {
        audioSource.clip = musicClip;
        secondsPerBeat = 60f / bpm;

        musicStartDspTime = AudioSettings.dspTime + backgroundMusicDelay;
        audioSource.PlayScheduled(musicStartDspTime);

        hasStarted = true;
        
        double scheduledStartTime = musicStartDspTime;
        double duration = audioSource.clip.length / audioSource.pitch;
        scheduledEndTime = scheduledStartTime + duration;

        HasEnded = false;

    }

    public void Stop()
    {
        hasStarted = false;
    }
 

    void Update()
    {
        if (!hasStarted) return;

        double currentDspTime = AudioSettings.dspTime;
        double songPosition = currentDspTime - musicStartDspTime;

        if (songPosition < 0) return;

        int currentBeat = Mathf.FloorToInt((float)(songPosition / secondsPerBeat));

        if (currentBeat > lastBeat)
        {
            lastBeat = currentBeat;
            OnBeat?.Invoke(currentBeat); // Gọi sự kiện beat
            // Debug.Log("Beat: " + currentBeat);
        }
        
        
       
        if (AudioSettings.dspTime >= scheduledEndTime - 0.1f)
        {
            HasEnded = true;
        }
    }

    public double GetDspTime() => AudioSettings.dspTime;

    public double GetSongPositionDsp() => AudioSettings.dspTime - musicStartDspTime;

    public float GetSecondsPerBeat() => secondsPerBeat;

    public int GetCurrentBeat()
    {
        if (!hasStarted) return 0;
        return Mathf.FloorToInt((float)(GetSongPositionDsp() / secondsPerBeat));
    }

    
}