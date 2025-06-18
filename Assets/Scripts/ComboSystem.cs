public class ComboSystem
{
    public int ComboCount { get; private set; }
    public int MaxCombo { get; private set; }

    public float Multiplier => 1f + ComboCount * 0.1f; 

    public void RegisterHit(HitResult result)
    {
        if (result == HitResult.Perfect || result == HitResult.Good)
        {
            ComboCount++;
            if (ComboCount > MaxCombo) MaxCombo = ComboCount;
        }
        else
        {
            ComboCount = 0;
        }
    }
}