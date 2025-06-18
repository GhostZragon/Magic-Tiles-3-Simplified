using UnityEngine;

public class RectCachedMono : PoolableObject
{
    public RectTransform RectTransform;
    
    protected virtual void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }
}