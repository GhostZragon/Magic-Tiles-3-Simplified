using Sirenix.OdinInspector;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    [SerializeField] private LineLane[] lines;
    [SerializeField] private Transform lineHit;
    [SerializeField] private float tileWidth = 1f;
    [SerializeField] private float verticalPadding = 0f;
    [SerializeField] private float lineSpacing = 0.2f;


    public LineLane GetLineTransform(int index)
    {
        return lines[index];
    }

    public LineLane GetRandomLineParent()
    {
        var randomIndex = Random.Range(0, lines.Length);
        return lines[randomIndex];
    }

    private void Start()
    {
        ResizeAndPositionLines();
    }

    [Button]
    private void ResizeAndPositionLines()
    {
        float cameraHeight = Camera.main.orthographicSize * 2f;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        float totalLaneWidth = (lines.Length * tileWidth) + ((lines.Length - 1) * lineSpacing);

        float startX = -totalLaneWidth / 2f + tileWidth / 2f;

        for (int i = 0; i < lines.Length; i++)
        {
            Transform line = lines[i].transform;
            if (line == null) continue;

            float x = startX + i * (tileWidth + lineSpacing);
            line.position = new Vector3(x, verticalPadding, 0f);

            SpriteRenderer sr = line.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                float spriteW = sr.sprite.bounds.size.x;
                float spriteH = sr.sprite.bounds.size.y;

                float targetH = cameraHeight;
                float targetW = tileWidth;

                line.localScale = new Vector3(
                    targetW / spriteW,
                    targetH / spriteH,
                    1f
                );
            }
        }
    }

    public float GetHitLinePercent()
    {
        var startY = lines[0].GetTopPosition().y;
        var endY = lines[0].GetBottomPosition().y;
        var hitLineY = lineHit.transform.position.y;

        float percent = Mathf.InverseLerp(startY, endY, hitLineY);
        Debug.Log("Get Hit Line Percent: " + percent);
        return percent;
    }
}