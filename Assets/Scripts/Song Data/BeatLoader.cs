using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Beat Loader", menuName = "Config/Beat Loader")]
public class BeatLoader : ScriptableObject
{
    public TextAsset beatFile;
    public List<float> beatTimes = new List<float>(); 

    [Button]
    private void Load()
    {
        beatTimes.Clear();

        if (beatFile == null)
        {
            Debug.LogError("Beat file is not assigned!");
            return;
        }

        string[] lines = beatFile.text.Split('\n');

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');

            if (parts.Length > 0)
            {
                if (float.TryParse(parts[0].Trim(), out float time))
                {
                    beatTimes.Add(time);
                }
                else
                {
                    Debug.LogWarning($"Failed to parse timestamp from line: {line}");
                }
            }
        }

        Debug.Log($"Loaded {beatTimes.Count} beat times successfully!");
    }
}