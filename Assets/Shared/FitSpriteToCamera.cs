using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class FitSpriteToCamera : MonoBehaviour
{
    public enum FitMode { Height, Width, Both }

    [Header("Settings")]
    public FitMode fitMode = FitMode.Height;
    public float heightMultiplier = 1f;
    public float widthMultiplier = 1f;
    public Camera targetCamera;

    private void Awake()
    {
        ApplyFit();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        ApplyFit();
    }
#endif

    private void ApplyFit()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        if (targetCamera == null) return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null) return;

        float camHeight = targetCamera.orthographicSize * 2f;
        float camWidth = camHeight * targetCamera.aspect;

        float spriteW = sr.sprite.bounds.size.x;
        float spriteH = sr.sprite.bounds.size.y;

        Vector3 scale = transform.localScale;

        if (fitMode == FitMode.Height || fitMode == FitMode.Both)
            scale.y = (camHeight * heightMultiplier) / spriteH;

        if (fitMode == FitMode.Width || fitMode == FitMode.Both)
            scale.x = (camWidth * widthMultiplier) / spriteW;

        transform.localScale = scale;
    }
}