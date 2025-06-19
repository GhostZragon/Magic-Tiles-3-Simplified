using UnityEngine;

public static class HitEvaluator
{
    public const float PERFECT_WINDOW = 0.15f;
    public const float GOOD_WINDOW = 0.25f;
    public const float MISS_WINDOW = 0.3f;

    public static HitResult Evaluate(float expectedTime, float actualTime)
    {
        float delta = Mathf.Abs(expectedTime - actualTime);
        if (delta <= PERFECT_WINDOW) return HitResult.Perfect;
        if (delta <= GOOD_WINDOW) return HitResult.Good;
        return HitResult.Miss;
    }
}