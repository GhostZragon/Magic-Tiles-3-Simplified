using System.Collections.Generic;
using UnityEngine;

public enum e_GameState
{
    None,
    Playing,
    Result
}
public class GameManager : UnitySingleton<GameManager>
{
    public e_ResultState GameResult;
    public e_GameState GameState;
    [Header("References")]
    [SerializeField] private MusicConductor conductor;
    [SerializeField] private ParticleEnvironmentManager particleEnvironmentManager;

    [SerializeField] private TileSpawner tileSpawner;
    [SerializeField] private LineSpawner lineSpawner;
    [Header("Settings")]
    [SerializeField] private int lineCounts = 4;

    [Header("Difficult Settings")]
    [SerializeField] private float fallDuration = 2f;

    [SerializeField] private float pitchMultiplier = 1;
    [SerializeField] private float bpm = 80f;

    private int playerTileTapCount = 0;
    private int totalTileTapToWin;
    
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
        
        // startGameSetup.Init(lineCounts);
        tileSpawner.ClearItemInPool();
        GameStateManager.Instance.ChangeState<GameplayState>();
    }


    public void StartGame()
    {
        // delay for first note
        float offset;
        upcomingNotes = noteDataLoader.LoadNoteQueue(out bpm, out offset);
        totalTileTapToWin = 0;
        playerTileTapCount = 0;
        if (upcomingNotes == null)
        {
            Debug.LogError("Up coming note is null, please check it again!", gameObject);
            return;
        }

        foreach (NoteData item in upcomingNotes)
        {
            if (item.type == NoteType.Double)
            {
                totalTileTapToWin += 2;
            }
            else
            {
                totalTileTapToWin += 1;
            }
        }
        
        // if background music start very soon, we should have delay to spawning
        conductor.Setup(bpm, pitchMultiplier,
            AdjustedBeatTime(upcomingNotes.Peek()) < AdjustedFallDuration ? AdjustedFallDuration : 0f);

        // starTimeBackgroundMusic = AudioSettings.dspTime;
        starTimeBackgroundMusic = conductor.AudioSource.time;
        
        particleEnvironmentManager.SetActiveState(true);
        
        // reset flag
        
        Debug.Log("On Start Game" +upcomingNotes.Count);
        GameState = e_GameState.Playing;
    }


    
    public void UpdateHandleNoteSpawning()
    {
        if (upcomingNotes.Count == 0)
        {
            return;
        }

        var peek = upcomingNotes.Peek();
        double adjustedTime = AdjustedBeatTime(peek);

        if (conductor.AudioSource.time >= starTimeBackgroundMusic + adjustedTime - AdjustedFallDuration)
        {
            tileSpawner.SpawnTile(peek, conductor.AudioSource, lineSpawner.GetRandomLineParent(), AdjustedFallDuration);
            upcomingNotes.Dequeue();
        }
    }

    public void AddTapCount()
    {
        // dont care about result of tap
        playerTileTapCount += 1;

        if (playerTileTapCount >= totalTileTapToWin)
        {
            GameEvent<EndGameEvent>.Raise(new EndGameEvent(e_ResultState.Win));
            
        }
    }

}


public struct EndGameEvent : IGameEvent
{
    public e_ResultState ResultState;

    public EndGameEvent(e_ResultState state) => ResultState = state;   
}