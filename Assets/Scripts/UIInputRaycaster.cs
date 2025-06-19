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
        Debug.Log($"[Input] Tap at {screenPos}");

        // Setup pointer event for raycasting
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = screenPos
        };

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerData, results);

        if (results.Count == 0)
        {
            Debug.LogWarning("[Raycast] Nothing hit!");
            return;
        }

        foreach (var result in results)
        {
            Debug.Log($"[Raycast] Hit: {result.gameObject.name}");
            BaseMusicTile tile = result.gameObject.GetComponent<BaseMusicTile>();
            if (tile != null)
            {
                Debug.Log("[Tile] Found BaseMusicTile – calling OnClick()");
                tile.OnClick();
                break;
            }
        }
    }
}