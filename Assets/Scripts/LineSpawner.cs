using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    private const float HEIGHT_SCALE_TILE = 0.15f;

    [SerializeField] private GameObject lineContainer;
    [SerializeField] private Transform[] lines;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private int lineCounts;

    [Button]
    public void InitLines()
    {

    }


    public Transform GetLineTransform(int index)
    {
        return lines[index];
    }

    public void SetLineCounts(int lineCounts)
    {
        this.lineCounts = lineCounts;
    }

    public Transform GetRandomLineParent()
    {
        var randomIndex = Random.Range(0, lineCounts);
        return lines[randomIndex];
    }
}
