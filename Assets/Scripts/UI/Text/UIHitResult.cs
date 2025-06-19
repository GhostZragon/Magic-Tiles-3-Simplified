using System;
using TMPro;
using UnityEngine;

public class UIHitResult : MonoBehaviour
{
    // use for debug
    
    [SerializeField] private bool isShow = false;
    [SerializeField] private RectTransform container;
    [SerializeField] private UIScoreText UIScoreTextPrefab;
    [SerializeField] private RectTransform spawnTransform;

    public TMP_ColorGradient missColor;
    public TMP_ColorGradient goodColor;
    public TMP_ColorGradient perfectColor ;

    private nObjectPool textPool;

    private void Awake()
    {
        textPool = ObjectPoolManager.GetObjectPool(UIScoreTextPrefab, 5);
        GameEvent<TileHitEvent>.Register(Get);

    }

    private void OnDestroy()
    {
        GameEvent<TileHitEvent>.Unregister(Get);
    }

    private void Get(TileHitEvent tileHitEvent)
    {
        if (isShow == false) return;
        
        var result = tileHitEvent.result;
        var popup = textPool.ReUse<UIScoreText>(spawnTransform.position, Quaternion.identity, container);
        
        popup.SetText(result.ToString(), GetColor(result));
        popup.Animate();
    }

    private TMP_ColorGradient GetColor(HitResult result)
    {
        switch (result)
        {
            case HitResult.Miss: return missColor;
            case HitResult.Good: return goodColor;
            case HitResult.Perfect: return perfectColor;
            default: return null;
        }
    }
}
