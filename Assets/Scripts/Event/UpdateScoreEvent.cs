public struct UpdateScoreEvent : IGameEvent
{
    public int newScore;
    public UpdateScoreEvent(int _newScore) => newScore = _newScore;
}