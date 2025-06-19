using System.Collections.Generic;

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