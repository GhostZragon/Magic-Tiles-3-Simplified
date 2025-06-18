using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class NoteDataLoader : MonoBehaviour
{
    // make sure have json dot in name, please
    [SerializeField] private List<string> beatMapList = new();
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
      
        beatmap.notes.Sort((a, b) => a.beatTime.CompareTo(b.beatTime));
        bpm = beatmap.bpm;
        offset = beatmap.offset;

        return new Queue<NoteData>(beatmap.notes);
    }

    [Button]
    private void GetName()
    {
        beatMapList.Clear();
        beatMapList = GetAvailableBeatmaps();
    }
    
    public List<string> GetAvailableBeatmaps()
    {
        List<string> beatmapFiles = new List<string>();
        string path = Application.streamingAssetsPath;

        if (!Directory.Exists(path))
        {
            Debug.LogError($"StreamingAssets folder not found at {path}");
            return beatmapFiles;
        }

        // Lấy tất cả file .json trong thư mục StreamingAssets
        string[] files = Directory.GetFiles(path, "*.json");
        foreach (string file in files)
        {
            // Chỉ lấy tên file (không lấy đường dẫn đầy đủ)
            string fileName = Path.GetFileName(file);
            beatmapFiles.Add(fileName);
        }

        return beatmapFiles;
    }
}