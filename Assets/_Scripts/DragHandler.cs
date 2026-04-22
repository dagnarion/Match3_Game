using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour
{
    [SerializeField] private GameBoard _gameBoard;
    private SwapHandler _swapHandler;
    Vector2 startPos;
    bool hasSwapped = false;
    private Camera mainCamera;
    float threshold = 0.2f;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        _swapHandler = _gameBoard.SwapHandler;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            hasSwapped = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (hasSwapped) return;
            Vector2 current = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 delta = current - startPos;
            
            if (delta.magnitude > threshold)
            {
                Vector2Int dir = GetDirection(delta);
                _swapHandler.SwapItem(startPos,dir);
                hasSwapped = true;
            }
        }
    }
    
    private Vector2Int GetDirection(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            return delta.x > 0 ? Vector2Int.right : Vector2Int.left;
        else
            return delta.y > 0 ? Vector2Int.up : Vector2Int.down;
    }
}
