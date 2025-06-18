using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int currentScore = 0;

    private void Awake()
    {
        GameEvent<UpdateScoreEvent>.Register(UpdateScore);
    }

    private void OnEnable()
    {
        scoreText.text = "";
    }

    private void OnDestroy()
    {
        GameEvent<UpdateScoreEvent>.Unregister(UpdateScore);
    }

    public void UpdateScore(UpdateScoreEvent updateScoreEvent)
    {
        int newScore = updateScoreEvent.newScore;
        if (newScore == currentScore) return;

        int oldScore = currentScore;
        currentScore = newScore;

        scoreText.text = currentScore.ToString();

        scoreText.transform.DOKill();
        scoreText.transform.localScale = Vector3.one * 1.3f;
        scoreText.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = "0";
    }
}