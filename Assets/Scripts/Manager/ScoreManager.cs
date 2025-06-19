using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private int perfectScore = 100;

    [SerializeField] private int goodScore = 50;
    [SerializeField] private int missScore = 15;
    [SerializeField] private bool debugMode = false;

    public int score { get; private set; }

    private ComboSystem comboSystem;

    private void Awake()
    {
        comboSystem = new();
        GameEvent<TileHitEvent>.Register(AddScore);
    }

    private void OnDestroy()
    {
        GameEvent<TileHitEvent>.Unregister(AddScore);
    }

    public void AddScore(TileHitEvent tileHitEvent)
    {
        var result = tileHitEvent.result;
        
        comboSystem.RegisterHit(result);
        var baseScore = result switch
        {
            HitResult.Perfect => perfectScore,
            HitResult.Good => goodScore,
            HitResult.Miss => missScore,
            _ => 0
        };
        var multiplier = comboSystem.Multiplier;
        score += Mathf.RoundToInt(baseScore * multiplier);

        if (debugMode)
        {
            Debug.Log(
                $"Hit: {result}, Combo: {comboSystem.ComboCount}, Multiplier: x{multiplier:F1}, Total Score: {score}");
        }

        GameEvent<ComboEvent>.Raise(new ComboEvent(comboSystem.ComboCount));
        GameEvent<UpdateScoreEvent>.Raise(new UpdateScoreEvent(score));
    }

    public void ResetScoreAndCombo()
    {
        score = 0;
        comboSystem.ResetComboCount();
    }
}