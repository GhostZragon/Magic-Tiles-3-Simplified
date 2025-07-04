﻿using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;


public class TileSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform singleTilePrefab;

    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private LineSpawner lineSpawner;
    private nObjectPool tilePool;

    public void Init()
    {
        if (!tilePool)
            tilePool = ObjectPoolManager.GetObjectPool(singleTilePrefab.GetComponent<SingleTile>(), 20);
    }

    public void ClearItemInPool()
    {
        if (tilePool)
            tilePool.RecoveryAll();
    }

    public void SpawnTile(NoteData noteData, AudioSource backgroundAudioSource, LineLane lineLane,
        float fallDuration)
    {
        float currentTime = backgroundAudioSource.time;
        float timeUntilHit = noteData.beatTime - currentTime;

        if (timeUntilHit <= 0)
        {
            Debug.LogWarning($"To late to spawn note at {noteData.ToString()}, then i will stop spawn this tile",
                gameObject);
            return;
        }

        // float parentHeight = parentTransform.position.y * 1.25f;
        // float fallDistance = -parentTransform.position.y;
        float fallPositionY = lineLane.GetBottomPosition().y;


        var tile = tilePool.ReUse<SingleTile>(lineLane.GetTopPosition(), singleTilePrefab.transform.rotation);

        tile.Init(
            hitTime: noteData.beatTime,
            hitPerent: lineSpawner.GetHitLinePercent(),
            fallDuration: fallDuration,
            fallPositionY: fallPositionY,
            audioSource: backgroundAudioSource,
            moveCurve: animationCurve
        );


        // tile.RectTransform.anchoredPosition = Vector2.zero;
        // tile.RectTransform
        //     .DOAnchorPos(
        //         new Vector2(0, -parent.GetComponent<RectTransform>().rect.height * 0.75f),
        //         fallDuration).OnComplete(() => { tile.RecoverSelf(); }).SetEase(Ease.Linear);
    }
}