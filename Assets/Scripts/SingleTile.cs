using DG.Tweening;
using UnityEngine.EventSystems;

public class SingleTile : BaseMusicTile
{
    protected override void OnClick()
    {
        base.OnClick();
        RecoverSelf();
    }
}