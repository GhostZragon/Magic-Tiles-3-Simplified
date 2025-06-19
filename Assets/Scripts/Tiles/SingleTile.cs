using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SingleTile : BaseMusicTile
{
    private static readonly int Tap = Animator.StringToHash("Tap");
    
    [SerializeField] private Animator buttonAnimator;
    public override void OnClick()
    {
        base.OnClick();
        CreateResultText();
        GameManager.Instance.AddTapCount();
        buttonAnimator.Play("Tap");
    }

    public override void OnReUse()
    {
        base.OnReUse();
        buttonAnimator.Play("Idle",0,0);
    }
}