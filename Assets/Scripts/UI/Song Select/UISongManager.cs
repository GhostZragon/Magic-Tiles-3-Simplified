using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISongManager : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private UISongView UISongPrefab;


    [SerializeField] private List<SongEntry> songEntries = new();
    
    public void InitSongList()
    {
        // in this time, just call one time
        UISongView.LoadSongEvent = OnLoadSongEntry;
        foreach (var item in songEntries)
        {
            var songUI = Instantiate(UISongPrefab, container);
            songUI.Setup(item);
        }
    }

    private void OnLoadSongEntry(SongEntry loadSongEntry)
    {
        GameEvent<LoadSongEvent>.Raise(new LoadSongEvent(loadSongEntry));
    }
}

public struct LoadSongEvent : IGameEvent
{
    public SongEntry songEntry;

    public LoadSongEvent(SongEntry _songEntry) => songEntry = _songEntry;
}