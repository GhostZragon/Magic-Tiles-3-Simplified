using System;
using UnityEngine;

public class UIScoreTextManager : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private UIScoreText UIScoreTextPrefab;

    public Color missColor = Color.gray;
    public Color goodColor = new Color(0.5f, 1f, 0.5f); // mint
    public Color perfectColor = Color.yellow;

    public static Action<RectTransform, HitResult> CreateTextAction;

    private nObjectPool textPool;

    private void Awake()
    {
        textPool = ObjectPoolManager.GetObjectPool(UIScoreTextPrefab, 10);
        CreateTextAction = Get;
    }

    private void Get(RectTransform target, HitResult result)
    {
        // Lấy parent gốc để đảm bảo cùng hierarchy
        var parent = target.parent;

        // Lấy vị trí anchored của target trong local space của parent
        Vector2 anchoredPos = target.anchoredPosition;

        // Spawn trong cùng parent, dùng vị trí anchored tương tự
        var popup = textPool.ReUse<UIScoreText>(anchoredPos, Quaternion.identity, parent);
        popup.SetText(result.ToString(), GetColor(result));
    }

    private Color GetColor(HitResult result)
    {
        switch (result)
        {
            case HitResult.Miss: return missColor;
            case HitResult.Good: return goodColor;
            case HitResult.Perfect: return perfectColor;
            default: return Color.white;
        }
    }
}
