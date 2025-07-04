﻿using System;
using DG.Tweening;
using UnityEngine;

public abstract class BaseMusicTile : PoolableObject
{
    [SerializeField] private SoundConfig clapSoundConfig;
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

        if (clapSoundConfig)
        {
            AudioManager.Instance.CreateSound(clapSoundConfig).Play();
        }
    }


    protected virtual void CreateResultText()
    {
        // create result based on hit time
        
        // TODO: Create another check based on position hitline like game ref
        var result = HitEvaluator.Evaluate(audioSource.time, hitTime);
        GameEvent<TileHitEvent>.Raise(new TileHitEvent(result));
        
        
    }

    private void OnDisable()
    {
        isInitialized = false;
        isClick = false;
    }

    public void Init(float hitTime,float hitPerent, float fallDuration, float fallPositionY, AudioSource audioSource,
        AnimationCurve moveCurve)
    {
        this.hitTime = hitTime;
        this.fallDuration = fallDuration;
        this.fallDistance = fallPositionY;
        this.audioSource = audioSource;
        this.moveCurve = moveCurve;

        float hitPercent = hitPerent;
        float timeToHitline = fallDuration * hitPercent;
        spawnTime = hitTime - timeToHitline;

        float startY = transform.position.y;
        float endY = fallPositionY;

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