using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseMusicTile : RectCachedMono, IPointerDownHandler
{
    private bool isClick = false;

    [SerializeField] protected Button btn;

    private float fallDuration;
    private float fallDistance;
    private float hitTime;
    private float spawnTime;
    private AudioSource audioSource;
    private AnimationCurve moveCurve;

    private Vector2 startAnchoredPos;
    private Vector2 endAnchoredPos;

    private bool isInitialized = false;

    protected override void Awake()
    {
        base.Awake();
        btn = GetComponent<Button>();

        RectTransform.anchorMax = new Vector2(0.5f, 1);
        RectTransform.anchorMin = new Vector2(0.5f, 1);
    }

    protected virtual void OnClick()
    {
        isClick = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick();
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

        startAnchoredPos = new Vector2(0f, startY);
        endAnchoredPos = new Vector2(0f, endY);

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
            if (isClick == false)
            {
                Debug.Log("ban da thua", gameObject);
                // GameEvent<EndGameEvent>.Raise(new EndGameEvent(e_ResultState.Lose));
            }

            RecoverSelf();
            RectTransform.anchoredPosition = endAnchoredPos;
            return;
        }

        float curvedT = moveCurve.Evaluate(t);
        RectTransform.anchoredPosition = Vector2.Lerp(startAnchoredPos, endAnchoredPos, curvedT);
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