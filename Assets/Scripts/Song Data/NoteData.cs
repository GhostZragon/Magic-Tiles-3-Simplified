using System;
using System.Collections.Generic;

[Serializable]
public struct NoteData
{
    public float beatTime;
    public float duration;
    public int lineIndex;
    public NoteType type;

}

public enum NoteType
{
    Tap,
    Hold,
    Slide,
    Flick
}
[System.Serializable]
public class BeatmapWrapper
{
    public string songName;
    public string artist;
    public float bpm; // depend of beatmap of song when split
    public float offset; // maybe is zero
    public float fallingDuration;
    public int difficultStar = 0;
    public List<NoteData> notes;
}