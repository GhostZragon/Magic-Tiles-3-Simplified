using Sirenix.OdinInspector;
using UnityEngine;

public class StartGameSetup : MonoBehaviour
{
    private const float START_TILE_OFFSET_FACTOR = 0.75f;
    
    [SerializeField] private LineSpawner lineSpawner;
    [SerializeField] private TileSpawner tileSpawner;
    [SerializeField] private StartTile startTilePrefab;
   

    private StartTile currentStartTileBtn;

    public void Init(int count)
    {
        SetupLinesAndSpawner(count);
        SetupStartTile();
    }
    
    private void SetupStartTile()
    {
        if (currentStartTileBtn)
        {
            Destroy(currentStartTileBtn.gameObject);
        }
        
        var parent = lineSpawner.GetRandomLineParent();
        currentStartTileBtn = Instantiate(startTilePrefab, parent);
        currentStartTileBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -parent.rect.height * START_TILE_OFFSET_FACTOR);
    }
    [Button]
    private void SetupLinesAndSpawner(int lineCounts)
    {
        lineSpawner.SetLineCounts(lineCounts);
        tileSpawner.Init(lineSpawner.GetTileWidth(), lineSpawner.GetTileHeight());
    }

}