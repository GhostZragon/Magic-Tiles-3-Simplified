using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : UnitySingleton<GameManager>
{
    [Header("References")]
    [SerializeField] private MusicConductor conductor;

    [SerializeField] private TileSpawner tileSpawner;
    [SerializeField] private LineSpawner lineSpawner;
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
        Invoke(nameof(Init), 1f);
    }
    
    [Button]
    public void Init()
    {
        var parent = GetRandomLineParent();
        var startTile = Instantiate(startTilePrefab, parent);
        startTile.RectTransform.anchoredPosition =
            new Vector2(0, -parent.GetComponent<RectTransform>().rect.height * 0.75f);

        lineSpawner.SetLineCounts(lineCounts);

        var width = lineSpawner.GetTileWidth();
        var height = lineSpawner.GetTileHeight();

        tileSpawner.Init(width, height);
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

        // if background music start very soon, we should have delay to spawning
        conductor.Setup(bpm, pitchMultiplier,
            AdjustedBeatTime(upcomingNotes.Peek()) < AdjustedFallDuration ? AdjustedFallDuration : 0f);

        // starTimeBackgroundMusic = AudioSettings.dspTime;
        starTimeBackgroundMusic = conductor.AudioSource.time;
    }

    private void Update()
    {
        if (upcomingNotes.Count > 0)
        {
            var peek = upcomingNotes.Peek();
            var adjustedFallDuration = fallDuration / pitchMultiplier;
            double adjustedTime = peek.beatTime / pitchMultiplier;

            // timing 
            if (conductor.AudioSource.time >= starTimeBackgroundMusic + adjustedTime - adjustedFallDuration)
            {
                tileSpawner.SpawnTile(peek, conductor.AudioSource, GetRandomLineParent(), adjustedFallDuration);
                upcomingNotes.Dequeue();
            }
        }
    }

    private RectTransform GetRandomLineParent()
    {
        var lineIndex = Random.Range(0, lineCounts);
        var parent = lineSpawner.GetLineTransform(lineIndex);

        return parent;
    }

    public void TryPlayAgain()
    {
        // stop time scale, wait player tap current note, when player tap note, continue play
    }
}