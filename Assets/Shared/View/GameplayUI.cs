using TMPro;
using UnityEngine;

public class GameplayUI : BaseView
{
    [SerializeField] private GameObject resultUI;
    [SerializeField] private TextMeshProUGUI resultScoreText;
    [SerializeField] private ScoreManager scoreManager;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        resultUI.gameObject.SetActive(false);
    }

    public void ShowResult()
    {
        resultUI.gameObject.SetActive(true);
        resultScoreText.text = $"{scoreManager.score}";
    }
}