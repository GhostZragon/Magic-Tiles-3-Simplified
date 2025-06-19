public struct ComboEvent : IGameEvent
{
    public int comboCount;
    public ComboEvent(int _comboCount) => comboCount = _comboCount;
}