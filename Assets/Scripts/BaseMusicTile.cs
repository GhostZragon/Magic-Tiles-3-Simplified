using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseMusicTile : RectCachedMono, IPointerDownHandler
{
    [SerializeField] protected Button btn;
    protected override void Awake()
    {
        base.Awake();
        btn = GetComponent<Button>();
        
        btn.onClick.AddListener(OnClick);

        RectTransform.anchorMax = new Vector2(0.5f, 1);
        RectTransform.anchorMin = new Vector2(0.5f, 1);
    }
    
    public override void OnReUse()
    {
        base.OnReUse();
    }
    
    protected virtual void OnClick()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick();
    }
}
