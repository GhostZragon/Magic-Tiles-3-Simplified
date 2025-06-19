using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class NoteDataLoader : MonoBehaviour
{
    public string jsonFileName = "beatmap_export";


    public Queue<NoteData> LoadNoteQueue(out float bpm, out float offset)
    {
        TextAsset jsonText = Resources.Load<TextAsset>("Song Data/" + jsonFileName);
        if (jsonText == null)
        {
            Debug.LogError($"Failed to load beatmap: {jsonFileName}");
            bpm = 120f;
            offset = 0f;
            return null;
        }

        BeatmapWrapper beatmap = JsonUtility.FromJson<BeatmapWrapper>(jsonText.text);
        bpm = beatmap.bpm;
        offset = beatmap.offset;
        return new Queue<NoteData>(beatmap.notes);
    }
}