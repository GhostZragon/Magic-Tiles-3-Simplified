using UnityEngine;

public class StartTile : BaseMusicTile
{
    [SerializeField] private Animator buttonAnimator;
    public override void OnClick()
    {
        base.OnClick();
        Invoke(nameof(DisableObject),1f);
        buttonAnimator?.Play("Tap");
    }

    private void DisableObject()
    {
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }
    
    public override void OnReUse()
    {
        base.OnReUse();
        buttonAnimator?.Play("Idle",0,0);
    }
}