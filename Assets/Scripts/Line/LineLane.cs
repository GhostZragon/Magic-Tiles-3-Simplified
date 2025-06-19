using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineLane : MonoBehaviour
{
    [SerializeField] private Transform topAnchor;
    [SerializeField] private Transform bottomAnchor;

    public Vector3 GetTopPosition() => topAnchor.position;
    public Vector3 GetBottomPosition() => bottomAnchor.position;

    public float GetTravelDistance() => Mathf.Abs(topAnchor.position.y - bottomAnchor.position.y);
}