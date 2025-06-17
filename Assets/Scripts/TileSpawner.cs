using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;


public class TileSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SingleTile singleTilePrefab;

    private nObjectPool tilePool;

    public void Init(float width, float height)
    {
        singleTilePrefab = Instantiate(singleTilePrefab, transform);
        singleTilePrefab.RectTransform.sizeDelta = new Vector2(width,height);
        singleTilePrefab.gameObject.SetActive(false);
        
        tilePool = ObjectPoolManager.GetObjectPool(singleTilePrefab, 20);
    }

    public void SpawnTile(int beat, Transform parent, float fallDuration)
    {
        var tile = tilePool.ReUse<SingleTile>(Vector3.zero, singleTilePrefab.transform.rotation, parent);
        tile.RectTransform.anchoredPosition = Vector2.zero;
        tile.RectTransform
            .DOAnchorPos(
                new Vector2(0, -parent.GetComponent<RectTransform>().rect.height + tile.RectTransform.rect.height / 2),
                fallDuration).OnComplete(() => { tile.RecoverSelf(); }).SetEase(Ease.Linear);

    }
}