using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class NoteDataLoader : MonoBehaviour
{
    // make sure have json dot in name, please
    public string jsonFileName = "beatmap_export.json";

    public Queue<NoteData> LoadNoteQueue(out float bpm, out float offset)
    {
        string path = Path.Combine(Application.streamingAssetsPath, jsonFileName);
        if (!File.Exists(path))
        {
            Debug.LogError($"File not found at {path}");
            bpm = 120f;
            offset = 0f;
            return null;
        }

        string json = File.ReadAllText(path);
        BeatmapWrapper beatmap = JsonUtility.FromJson<BeatmapWrapper>(json);
      
        // beatmap.notes.Sort((a, b) => a.beatTime.CompareTo(b.beatTime));
        bpm = beatmap.bpm;
        offset = beatmap.offset;

        return new Queue<NoteData>(beatmap.notes);
    }
}