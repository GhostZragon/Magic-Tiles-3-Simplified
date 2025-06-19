using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Playing,
    Result
}
public class GameManager : UnitySingleton<GameManager>
{
    private const float startTileOffsetFactor = 0.75f;

    [Header("References")]
    [SerializeField] private MusicConductor conductor;

    [SerializeField] private ParticleEnvironmentManager particleEnvironmentManager;
    [SerializeField] private TileSpawner tileSpawner;
    [SerializeField] private LineSpawner lineSpawner;
    [Header("Prefabs")]
    [SerializeField] private StartTile startTilePrefab;

    [Header("Settings")]
    [SerializeField] private int lineCounts = 4;

    [Header("Difficult Settings")]
    [SerializeField] private float fallDuration = 2f;

    [SerializeField] private float pitchMultiplier = 1;
    [SerializeField] private float bpm = 80f;
    private NoteDataLoader noteDataLoader;
    private Queue<NoteData> upcomingNotes = new();

    private double starTimeBackgroundMusic;
    private float AdjustedFallDuration => fallDuration / pitchMultiplier;
    private double AdjustedBeatTime(NoteData note) => note.beatTime / pitchMultiplier;

    private void Start()
    {
        noteDataLoader = GetComponent<NoteDataLoader>();
        GameEvent<LoadSongEvent>.Register(HandleSongLoadEvent);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameEvent<LoadSongEvent>.Unregister(HandleSongLoadEvent);
        
    }

    private void HandleSongLoadEvent(LoadSongEvent obj)
    {
        // TODO: Make Sure Unload in future if have time
        // Just load beat map and audio clip
        noteDataLoader.jsonFileName = obj.songEntry.jsonFileName;
        conductor.musicClip = Resources.Load<AudioClip>($"Audio/{obj.songEntry.audioFileName}");
    
        // using addressable for optimize  memory usage
        // can use async method for waiting data load
        Init();
    }

    private void Init()
    {
        SetupStartTile();
        SetupLinesAndSpawner();
    }

    private StartTile currentStartTileBtn;
    private void SetupStartTile()
    {
        if (currentStartTileBtn)
        {
            Destroy(currentStartTileBtn.gameObject);
        }
        
        var parent = GetRandomLineParent();
        currentStartTileBtn = Instantiate(startTilePrefab, parent);
        currentStartTileBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -parent.rect.height * startTileOffsetFactor);
    }

    private void SetupLinesAndSpawner()
    {
        lineSpawner.SetLineCounts(lineCounts);
        tileSpawner.Init(lineSpawner.GetTileWidth(), lineSpawner.GetTileHeight());
    }

    public void StartGame()
    {
        // delay for first note
        float offset;
        upcomingNotes = noteDataLoader.LoadNoteQueue(out bpm, out offset);

        if (upcomingNotes == null)
        {
            Debug.LogError("Up coming note is null, please check it again!", gameObject);
            return;
        }
        // reset flag
        
        winCallFlag = false;
        isPlayDone = false;
        
        // if background music start very soon, we should have delay to spawning
        conductor.Setup(bpm, pitchMultiplier,
            AdjustedBeatTime(upcomingNotes.Peek()) < AdjustedFallDuration ? AdjustedFallDuration : 0f);

        // starTimeBackgroundMusic = AudioSettings.dspTime;
        starTimeBackgroundMusic = conductor.AudioSource.time;
        
        particleEnvironmentManager.SetActiveState(true);
    }

    private void Update()
    {
        HandleNoteSpawning();
    }

    private bool winCallFlag = false;
    private bool isPlayDone;
    private void HandleNoteSpawning()
    {
        if (isPlayDone) return;
        
        if (upcomingNotes.Count == 0)
        {
            if (winCallFlag == false)
            {
                winCallFlag = true;
                particleEnvironmentManager.SetActiveState(false);
                GameEvent<WinGameEvent>.Raise(new WinGameEvent());
            }
            return;
        }

        var peek = upcomingNotes.Peek();
        double adjustedTime = AdjustedBeatTime(peek);

        if (conductor.AudioSource.time >= starTimeBackgroundMusic + adjustedTime - AdjustedFallDuration)
        {
            tileSpawner.SpawnTile(peek, conductor.AudioSource, GetRandomLineParent(), AdjustedFallDuration);
            upcomingNotes.Dequeue();
        }
    }
    private RectTransform GetRandomLineParent()
    {
        var lineIndex = Random.Range(0, lineCounts);
        var parent = lineSpawner.GetLineTransform(lineIndex);

        return parent;
    }

    public void Stop()
    {
        isPlayDone = true;
    }
}

public struct WinGameEvent : IGameEvent
{
    
}

public struct LoseGameEvent : IGameEvent
{
    
}