using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SingleTile : BaseMusicTile
{
    private static readonly int Tap = Animator.StringToHash("Tap");
    
    [SerializeField] private Animator buttonAnimator;
    protected override void OnClick()
    {
        base.OnClick();
        CreateResultText();
        buttonAnimator.Play("Tap");
    }

    public override void OnReUse()
    {
        base.OnReUse();
        buttonAnimator.Play("Idle",0,0);
    }
}