using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : BaseView
{
    [SerializeField] private GameObject resultUI;
    [SerializeField] private TextMeshProUGUI resultScoreText;
    [SerializeField] private Button mainMenuBtn;

    private void Awake()
    {
        mainMenuBtn.onClick.AddListener(() =>
        {
            GameStateManager.Instance.ChangeState<MainMenuState>();
        });
    }


    public override Task Show(bool isAnimated = false)
    {
        resultUI.gameObject.SetActive(false);
        return base.Show(isAnimated);
    }

    public void ShowResult()
    {
        resultUI.gameObject.SetActive(true);
        resultScoreText.text = $"{GameStateManager.GameContext.ScoreManager.score}";
    }
}