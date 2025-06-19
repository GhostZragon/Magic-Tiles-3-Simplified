using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] lines;

    public Transform GetLineTransform(int index)
    {
        return lines[index];
    }

    public Transform GetRandomLineParent()
    {
        var randomIndex = Random.Range(0, lines.Length);
        return lines[randomIndex];
    }
}
