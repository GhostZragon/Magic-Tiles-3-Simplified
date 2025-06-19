using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseMusicTile : PoolableObject
{
    private bool isClick = false;


    private float fallDuration;
    private float fallDistance;
    private float hitTime;
    private float spawnTime;
    private AudioSource audioSource;
    private AnimationCurve moveCurve;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private bool isInitialized = false;


    public virtual void OnClick()
    {
        if (isClick) return;
        isClick = true;
    }


    protected virtual void CreateResultText()
    {
        var result = HitEvaluator.Evaluate(audioSource.time, hitTime);
        GameEvent<TileHitEvent>.Raise(new TileHitEvent(result));
    }

    private void OnDisable()
    {
        isInitialized = false;
        isClick = false;
    }

    public void Init(float hitTime, float fallDuration, float fallDistance, AudioSource audioSource,
        AnimationCurve moveCurve)
    {
        this.hitTime = hitTime;
        this.fallDuration = fallDuration;
        this.fallDistance = fallDistance;
        this.audioSource = audioSource;
        this.moveCurve = moveCurve;

        float hitPercent = 0.75f;
        float timeToHitline = fallDuration * hitPercent;
        spawnTime = hitTime - timeToHitline;

        float startY = 0f;
        float endY = -fallDistance;

        Vector3 currentPos = transform.position;
       
        startPosition = new Vector3(currentPos.x, startY, currentPos.z);
        endPosition = new Vector3(currentPos.x, endY, currentPos.z);
     
        transform.position = startPosition;

        isInitialized = true;
    }

    void Update()
    {
        if (!isInitialized) return;

        float audioTime = audioSource.time;
        float t = (audioTime - spawnTime) / fallDuration;

        if (t < 0f)
            return;

        if (t > 1f)
        {
            if (!isClick)
            {
                Debug.Log("Bạn đã thua", gameObject);
                GameEvent<EndGameEvent>.Raise(new EndGameEvent(e_ResultState.Lose));
            }

            RecoverSelf();
            transform.position = endPosition;
            return;
        }

        float curvedT = moveCurve.Evaluate(t);
        transform.position = Vector3.Lerp(startPosition, endPosition, curvedT);
    }
    
}

public enum HitResult
{
    Miss,
    Good,
    Perfect
}

public static class HitEvaluator
{
    public const float PERFECT_WINDOW = 0.15f;
    public const float GOOD_WINDOW = 0.25f;
    public const float MISS_WINDOW = 0.3f;

    public static HitResult Evaluate(float expectedTime, float actualTime)
    {
        float delta = Mathf.Abs(expectedTime - actualTime);
        if (delta <= PERFECT_WINDOW) return HitResult.Perfect;
        if (delta <= GOOD_WINDOW) return HitResult.Good;
        return HitResult.Miss;
    }
}