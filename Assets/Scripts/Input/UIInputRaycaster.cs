using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class UIInputRaycaster : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster graphicRaycaster;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Camera uiCamera;

    private InputHandler inputActions;

    private void Awake()
    {
        inputActions = new InputHandler();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Tap.performed += OnTap;
    }

    private void OnDisable()
    {
        inputActions.Player.Tap.performed -= OnTap;
        inputActions.Player.Disable();
    }

    private void OnTap(InputAction.CallbackContext context)
    {
        Vector2 screenPos = inputActions.Player.Point.ReadValue<Vector2>();
        Vector2 worldPos = uiCamera.ScreenToWorldPoint(screenPos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log($"[Raycast2D] Hit: {hit.collider.name}");
            BaseMusicTile tile = hit.collider.GetComponentInParent<BaseMusicTile>();
            if (tile != null)
            {
                Debug.Log("[Tile] Found BaseMusicTile – calling OnClick()");
                tile.OnClick();
            }
        }
        else
        {
            Debug.LogWarning("[Raycast2D] Nothing hit!");
        }
    }
}