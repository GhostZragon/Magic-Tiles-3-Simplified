public class StartTile : BaseMusicTile
{
    public override void OnClick()
    {
        base.OnClick();
        GameManager.Instance.StartGame();
        Destroy(gameObject,0.1f);
    }
}