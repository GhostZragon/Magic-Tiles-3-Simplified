public struct EndGameEvent : IGameEvent
{
    public e_ResultState ResultState;

    public EndGameEvent(e_ResultState state) => ResultState = state;   
}