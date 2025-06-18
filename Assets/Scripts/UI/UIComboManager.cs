using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIComboManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private GameObject comboPanel;
    [SerializeField] private float hideDelay = 2f;

    private Coroutine hideRoutine;
    private Tween scaleTween;

    private void Awake()
    {
        comboText.text = "";
        comboPanel.SetActive(false);
        GameEvent<ComboEvent>.Register(ShowCombo);
    }

    private void OnDestroy()
    {
        GameEvent<ComboEvent>.Unregister(ShowCombo);
    }

    public void ShowCombo(ComboEvent comboEvent)
    {
        int comboCount = comboEvent.comboCount;

        if (comboCount <= 1)
        {
            HideCombo();
            return;
        }

        if (!comboPanel.activeSelf)
        {
            comboPanel.SetActive(true);
            comboPanel.transform.localScale = Vector3.zero;
            comboPanel.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
        }

        comboText.text = $"x{comboCount}";

        scaleTween?.Kill();
        comboText.color = GetComboColor(comboCount);
    
        comboText.transform.localScale = Vector3.one * 1.5f;
        scaleTween = comboText.transform
            .DOScale(1f, 0.25f)
            .SetEase(Ease.OutBack);

        if (hideRoutine != null) StopCoroutine(hideRoutine);
        hideRoutine = StartCoroutine(HideAfterDelay());
    }
    
    private Color GetComboColor(int comboCount)
    {
        if (comboCount < 10)
            return Color.white;

        float t = Mathf.InverseLerp(10f, 70f, comboCount);

        return Color.Lerp(new Color(1f, 0.85f, 0.2f), new Color(0f, 1f, 1f), t);
    }

    private IEnumerator HideAfterDelay()
    {
        yield return Yielders.GetWaitForSeconds(hideDelay);
        HideCombo();
    }
    private void HideCombo()
    {
        scaleTween?.Kill();
        Sequence hideSeq = DOTween.Sequence();
        hideSeq.Append(comboText.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack));
        hideSeq.Join(comboPanel.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack));
        hideSeq.OnComplete(() =>
        {
            comboPanel.SetActive(false);
            comboText.text = "";
        });
    }
}