public struct TileHitEvent : IGameEvent
{
    public HitResult result;
    public TileHitEvent(HitResult _result) => result = _result;
}