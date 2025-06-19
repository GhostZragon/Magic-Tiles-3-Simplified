public class StartTile : BaseMusicTile
{
    protected override void OnClick()
    {
        base.OnClick();
        GameManager.Instance.StartGame();
        Destroy(gameObject,0.1f);
    }
}