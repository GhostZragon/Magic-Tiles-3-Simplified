using Sirenix.OdinInspector;
using UnityEngine;

public class StartGameSetup : MonoBehaviour
{
    private const float START_TILE_OFFSET_FACTOR = 0.55f;
    
    [SerializeField] private LineSpawner lineSpawner;
    [SerializeField] private TileSpawner tileSpawner;
    [SerializeField] private StartTile startTilePrefab;
   

    private StartTile currentStartTileBtn;

    public void Init(int count)
    {
        tileSpawner.Init();
        SetupStartTile();
    }
    
    private void SetupStartTile()
    {
        if (currentStartTileBtn)
        {
            Destroy(currentStartTileBtn.gameObject);
        }
        
        var parent = lineSpawner.GetRandomLineParent();
        currentStartTileBtn = Instantiate(startTilePrefab);
        currentStartTileBtn.transform.position = new Vector3(parent.transform.position.x,
            parent.transform.position.y * -START_TILE_OFFSET_FACTOR);
    }

}