using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISongView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI informationText;
    [SerializeField] private Button playBtn;
    
    [SerializeField] private SongEntry songEntry;
    public static Action<SongEntry> LoadSongEvent;

    private void Awake()
    {
        playBtn.onClick.AddListener(OnClickPlay);
    }

    private void OnDestroy()
    {
        playBtn.onClick.RemoveListener(OnClickPlay);
    }


    public void Setup(SongEntry songEntry)
    {
        this.songEntry = songEntry;
        
        informationText.text =
            $"Song Name: {songEntry.displayName}\n" +
            $"Artist: {songEntry.artist}\n" +
            $"BPM: {songEntry.bpm}";
    }

    private void OnClickPlay()
    {
        LoadSongEvent?.Invoke(songEntry);
    }
}