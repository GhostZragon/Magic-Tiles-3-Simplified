using UnityEngine;

public class MainMenuUI : BaseView
{
   [SerializeField] private UISongManager uiSongManager;

   private void Awake()
   {
      uiSongManager = GetComponentInChildren<UISongManager>();
   }

   private void Start()
   {
      uiSongManager.InitSongList();
   }
}
