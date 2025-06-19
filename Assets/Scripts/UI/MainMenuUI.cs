using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseView
{
   public UISongManager uiSongManager;
   [SerializeField] private Button exitBtn;

   private void Awake()
   {
      exitBtn.onClick.AddListener(() =>
      {
         Application.Quit();
      });
   }
}
