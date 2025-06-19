using System.Collections.Generic;
using UnityEngine;

public class BeatManager : UnitySingleton<BeatManager>
{
    [SerializeField] private float bpm = 80f;
    [SerializeField] private AudioSource audioSource;

    private List<BeatListener> listeners = new List<BeatListener>();

    public void RegisterListener(BeatListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(BeatListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }

    private void Update()
    {
        if (audioSource == null || !audioSource.isPlaying)
            return;

        float timeInSeconds = audioSource.timeSamples / (float)audioSource.clip.frequency;
        float beatsPassed = timeInSeconds * (bpm / 60f); 

        foreach (var listener in listeners)
        {
            listener.CheckAndTrigger(beatsPassed);
        }
    }

    public void SetBpm(float newBpm)
    {
        bpm = newBpm;
    }
}