using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    private const float HEIGHT_SCALE_TILE = 0.15f;
    
    [SerializeField] private GameObject lineContainer;
    [SerializeField] private RectTransform[] lines;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private int lineCounts;

    [Button]
    public void InitLines()
    {
        Clear();
        lines = new RectTransform[lineCounts];
        for (int i = 0; i < lineCounts; i++)
        {
            lines[i] = Instantiate(linePrefab, lineContainer.transform).GetComponent<RectTransform>();
        }
    }
    
    private void Clear()
    {
        if (lines != null)
        {
            foreach (var item in lines)
            {
                if (Application.isPlaying)
                {
                    Destroy(item.gameObject);
                }
                else
                {
                    DestroyImmediate(item.gameObject);
                }
            }
        }
    }

    public Transform GetLineTransform(int index)
    {
        return lines[index];
    }

    public void SetLineCounts(int lineCounts)
    {
        this.lineCounts = lineCounts;
    }

    public float GetTileWidth()
    {
        return lines[0].rect.width;
    }
    
    public float GetTileHeight()
    {
        return lines[0].rect.height * HEIGHT_SCALE_TILE;
    }
}
