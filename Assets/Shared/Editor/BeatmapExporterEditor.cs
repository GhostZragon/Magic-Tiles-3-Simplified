using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class BeatmapExporterEditor : EditorWindow
{
    private BeatLoader beatLoader;

    private string songName = "Default Song";
    private string artist = "Unknown";
    private float bpm = 120f;
    private float offset = 0f;
    private float fallDuration = 0f;
    private int difficultStar = 1;

    private bool useAutoFileName = true;
    private string customFileName = "beatmap_export.json";

    private string previewFileName => GenerateFileName();

    [MenuItem("Tools/Beatmap Exporter")]
    public static void ShowWindow()
    {
        GetWindow<BeatmapExporterEditor>("Beatmap Exporter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Export BeatLoader to Beatmap JSON", EditorStyles.boldLabel);
        beatLoader = (BeatLoader)EditorGUILayout.ObjectField("BeatLoader", beatLoader, typeof(BeatLoader), false);

        songName = EditorGUILayout.TextField("Song Name", songName);
        artist = EditorGUILayout.TextField("Artist", artist);
        bpm = EditorGUILayout.FloatField("BPM", bpm);
        offset = EditorGUILayout.FloatField("Offset", offset);
        fallDuration = EditorGUILayout.FloatField("Fall Duration", fallDuration);
        difficultStar = EditorGUILayout.IntSlider("Difficult Star", difficultStar, 1, 10);

        EditorGUILayout.Space(10);
        useAutoFileName = EditorGUILayout.Toggle("Use Auto File Name", useAutoFileName);

        if (!useAutoFileName)
        {
            customFileName = EditorGUILayout.TextField("Custom File Name", customFileName);
        }

        EditorGUILayout.HelpBox($"Preview: {previewFileName}", MessageType.Info);

        EditorGUILayout.Space();
        if (GUILayout.Button("Export"))
        {
            Export();
        }
    }

    private string GenerateFileName()
    {
        if (!useAutoFileName)
        {
            return SanitizeFileName(customFileName.EndsWith(".json") ? customFileName : $"{customFileName}.json");
        }

        string cleanName = SanitizeFileName(songName.ToLower().Replace(" ", "_"));
        return $"{cleanName}_{Mathf.RoundToInt(bpm)}_{fallDuration:0.00}_{difficultStar}stars.json";
    }

    private string SanitizeFileName(string fileName)
    {
        string invalidChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
        string regex = $"[{Regex.Escape(invalidChars)}]";
        return Regex.Replace(fileName, regex, "_");
    }

    private void Export()
    {
        if (beatLoader == null || beatLoader.beatTimes.Count == 0)
        {
            Debug.LogError("BeatLoader not assigned or empty.");
            return;
        }

        BeatmapWrapper beatmap = new BeatmapWrapper
        {
            songName = songName,
            artist = artist,
            bpm = bpm,
            offset = offset,
            fallingDuration = fallDuration,
            difficultStar = difficultStar,
            notes = new List<NoteData>()
        };

        int lineCount = 4;
        int lastLineIndex = -1;
        int sameLineCount = 0;

        for (int i = 0; i < beatLoader.beatTimes.Count; i++)
        {
            int lineIndex;

            // random line index that different from the last one if we have 2 in a row
            if (sameLineCount >= 1)
            {
                List<int> availableLines = new List<int>();
                for (int j = 0; j < lineCount; j++)
                {
                    if (j != lastLineIndex)
                    {
                        availableLines.Add(j);
                    }
                }

                lineIndex = availableLines[Random.Range(0, availableLines.Count)];
            }
            else
            {
                lineIndex = Random.Range(0, lineCount);
            }

            if (lineIndex == lastLineIndex)
            {
                sameLineCount++;
            }
            else
            {
                sameLineCount = 0;
                lastLineIndex = lineIndex;
            }

            beatmap.notes.Add(new NoteData
            {
                beatTime = beatLoader.beatTimes[i],
                duration = 0,
                lineIndex = lineIndex,
                type = NoteType.Single,
            });
        }

        string json = JsonUtility.ToJson(beatmap, true);
        string folderPath = Path.Combine(Application.dataPath, "StreamingAssets");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string fullPath = Path.Combine(folderPath, previewFileName);
        File.WriteAllText(fullPath, json);

        Debug.Log($"Exported Beatmap to: {fullPath}");
        AssetDatabase.Refresh();
    }
}