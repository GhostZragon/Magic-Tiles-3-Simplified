using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    private const float HEIGHT_SCALE_TILE = 0.15f;

    [SerializeField] private Transform[] lines;

    [Button]
    public void InitLines()
    {

    }


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
