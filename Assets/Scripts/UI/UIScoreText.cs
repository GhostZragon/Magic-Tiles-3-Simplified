using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIScoreText : RectCachedMono
{
    [SerializeField] private TextMeshProUGUI scoreText;

    protected override void Awake()
    {
        base.Awake();
        scoreText.text = "";
    }

    public void SetText(string text, TMP_ColorGradient color)
    {
        scoreText.text = text;
        scoreText.colorGradientPreset = color;
    }
    
    public void Animate()
    {
        RectTransform.localScale = Vector3.zero;
        scoreText.alpha = 1f;

        Vector3 startPos = RectTransform.anchoredPosition;
        Vector3 endPos = startPos - new Vector3(0f, 20f, 0f);

        Sequence seq = DOTween.Sequence();
        seq.Append(RectTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack));
        // seq.Join(RectTransform.DOLocalMove(endPos, 0.7f).SetEase(Ease.OutCubic));
        seq.Join(scoreText.DOFade(0f, 0.7f).SetEase(Ease.InOutQuad));
        seq.OnComplete(() => ReturnToPool());
    }

    private void ReturnToPool()
    {
        RecoverSelf();
    }
}