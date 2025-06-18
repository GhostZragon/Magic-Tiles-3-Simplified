using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Intervals
{
    [SerializeField] private float stepsPerBeat = 1f;        
    [SerializeField] private UnityEvent onIntervalTriggered; 

    private int lastIntervalIndex = -1;                      

    public float GetIntervalLength(float bpm)
    {
        return 60f / (bpm * stepsPerBeat);
    }

    public void CheckForNewInterval(float currentIntervalTime)
    {
        int currentIndex = Mathf.FloorToInt(currentIntervalTime);

        if (currentIndex != lastIntervalIndex)
        {
            lastIntervalIndex = currentIndex;
            onIntervalTriggered?.Invoke();
        }
    }
}