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
        string[] lines = beatFile.text.Split('\n');
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            string[] parts = line.Split('\t');
            if (float.TryParse(parts[0], out float time))
            {
                beatTimes.Add(time);
            }
        }
    }
}
