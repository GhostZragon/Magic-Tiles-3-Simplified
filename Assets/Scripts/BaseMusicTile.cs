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
        UIScoreTextManager.CreateTextAction(RectTransform, EvaluateHit(audioSource.time, hitTime));
    }

    private HitResult EvaluateHit(float currentTime, float targetTime)
    {
        float timeOffset = Mathf.Abs(currentTime - targetTime);

        const float perfectThreshold = 0.05f;
        const float goodThreshold = 0.12f;

        if (timeOffset <= perfectThreshold)
            return HitResult.Perfect;
        else if (timeOffset <= goodThreshold)
            return HitResult.Good;
        else
            return HitResult.Miss;
    }

    private void OnDisable()
    {
        isInitialized = false;
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
            CreateResultText();
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