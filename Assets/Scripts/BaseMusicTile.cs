using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseMusicTile : RectCachedMono, IPointerDownHandler
{
    public float spawnTime;
    public float targetTime;
    public float startY;
    public float endY;
    public bool isActive;
    public AudioSource backgroundSoundAudioSource;

    private bool isClick = false;

    [SerializeField] protected Button btn;

    protected override void Awake()
    {
        base.Awake();
        btn = GetComponent<Button>();

        btn.onClick.AddListener(OnClick);

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

    public void Init(float beatTime, float fallDuration, float startY, float endY, AudioSource audio)
    {
        this.backgroundSoundAudioSource = audio;
        this.spawnTime = beatTime - fallDuration;
        this.targetTime = beatTime;
        this.startY = startY;
        this.endY = endY;
        isActive = true;
    }

    void Update()
    {
        if (!isActive) return;

        float currentTime = backgroundSoundAudioSource.time;

        if (currentTime >= targetTime)
        {
            RectTransform.anchoredPosition = new Vector2(0, endY);
            isActive = false;
            return;
        }

        float progress = Mathf.InverseLerp(spawnTime, targetTime, currentTime);
        float currentY = Mathf.Lerp(startY, endY, progress);

        if (isClick)
        {
            Debug.Log("Hit point is: " + EvaluateHit(currentTime, targetTime));
            isClick = false;
        }

        RectTransform.anchoredPosition = new Vector2(0, currentY);
    }

    public HitResult EvaluateHit(float currentTime, float targetTime)
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
}

public enum HitResult
{
    Miss,
    Good,
    Perfect
}