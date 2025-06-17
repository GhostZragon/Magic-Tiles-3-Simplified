using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager>
{
    [Header("References")]
    [SerializeField] private MusicConductor conductor;
    [SerializeField] private TileSpawner tileSpawner;
    [SerializeField] private LineSpawner lineSpawner;
    [SerializeField] private StartTile startTilePrefab;
    [Header("Settings")]
    [SerializeField] private float backgroundMusicDelay = 1;
    [SerializeField] private int lineCounts = 4;
    
    [Header("Difficult Settings")]
    [SerializeField] private float fallDuration = 2f;
    [SerializeField] private float bpm = 60f;

    private int nextSpawnBeat;


    private void Start()
    {
        Invoke(nameof(Init), 2f);
    }

    [Button]
    public void Init()
    {
        var parent = GetRandomLineParent();
        var startTile = Instantiate(startTilePrefab, parent);
        
        startTile.RectTransform.anchoredPosition =
            new Vector2(0, - parent.GetComponent<RectTransform>().rect.height * 0.75f);
        
        lineSpawner.SetLineCounts(lineCounts);
        
        var width = lineSpawner.GetTileWidth();
        var height = lineSpawner.GetTileHeight();
        
        tileSpawner.Init(width, height);
    }

    public void StartGame()
    {
        nextSpawnBeat = -1;
        conductor.Setup(bpm, backgroundMusicDelay);
    }


    private void Update()
    {
        if (conductor.HasEnded) return;
        
        int currentBeat = conductor.GetCurrentBeat();
        double songPos = conductor.GetSongPositionDsp();
        float secondsPerBeat = conductor.GetSecondsPerBeat();

        // Dự đoán khi nào tile sẽ đến "vùng nhấn"
        int lookaheadBeats = Mathf.CeilToInt(fallDuration / secondsPerBeat) + 1;
        
        while (currentBeat + lookaheadBeats > nextSpawnBeat) // spawn sớm trước 4 beats
        {
            double timeToHit = nextSpawnBeat * secondsPerBeat;
            double spawnTime = timeToHit - fallDuration;

            if (spawnTime >= conductor.GetSongPositionDsp())
            {
                var parent = GetRandomLineParent();
                tileSpawner.SpawnTile(nextSpawnBeat, parent, fallDuration);
            }

            nextSpawnBeat++;
        }
    }

    private Transform GetRandomLineParent()
    {
        var lineIndex = Random.Range(0, lineCounts);
        var parent = lineSpawner.GetLineTransform(lineIndex);

        return parent;
    }
}