using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseMusicTile : RectCachedMono, IPointerDownHandler
{
    public float spawnTime;
    public float targetTime;
    public float startY;
    public float hitY;
    public bool isActive;
    public AudioSource backgroundSoundAudioSource;
    
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
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick();
    }
    
    public void Init(float beatTime, float fallDuration, float startY, float hitY, AudioSource audio)
    {
        this.backgroundSoundAudioSource = audio;
        this.spawnTime = beatTime - fallDuration;
        this.targetTime = beatTime;
        this.startY = startY;
        this.hitY = hitY;
        isActive = true;
    }
    
    void Update()
    {
        if (!isActive) return;

        float currentTime = backgroundSoundAudioSource.time;

        if (currentTime >= targetTime)
        {
            RectTransform.anchoredPosition = new Vector2(0, hitY);
            isActive = false; 
            return;
        }

        float progress = Mathf.InverseLerp(spawnTime, targetTime, currentTime);
        float currentY = Mathf.Lerp(startY, hitY, progress);

        RectTransform.anchoredPosition = new Vector2(0, currentY);
    }
}
