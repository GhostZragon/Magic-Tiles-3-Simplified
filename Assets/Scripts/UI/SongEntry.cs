using UnityEngine;

[CreateAssetMenu(fileName = "SongEntry", menuName = "Game/Song Entry")]
public class SongEntry : ScriptableObject
{
    [Header("Meta")]
    public string songId;
    public string displayName;
    public string artist = "Unknow";
    [Header("Asset name")]
    public string audioFileName;    
    public string jsonFileName;   

    [Header("Preview")]
    public Sprite thumbnail;
    public float bpm = 80;
}