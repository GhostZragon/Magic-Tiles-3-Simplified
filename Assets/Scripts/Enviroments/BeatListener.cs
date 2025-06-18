using System;
using UnityEngine;
using UnityEngine.Events;

public class BeatListener : MonoBehaviour
{
    [SerializeField] private float stepsPerBeat = 1f;
    [SerializeField] private UnityEvent onBeat;

    private int lastStep = -1;
    private BeatManager beatManager;

    public float StepsPerBeat => stepsPerBeat;

    private void OnEnable()
    {
        beatManager = BeatManager.Instance;
        beatManager?.RegisterListener(this);
    }

    private void OnDisable()
    {
        beatManager?.UnregisterListener(this);
    }

    public void CheckAndTrigger(float beatTime)
    {
        int currentStep = Mathf.FloorToInt(beatTime / stepsPerBeat);
        if (currentStep != lastStep)
        {
            lastStep = currentStep;
            onBeat?.Invoke();
        }
    }
}