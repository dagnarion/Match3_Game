using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SwapingController
{
    private GridConfig config;
    private SwapingModel swapingModel;
    private Camera mainCam;
    private Vector2 mouseDownPosition;
    private Vector2Int currentCell;
    private bool HadSwap;
    private float thresHold = 0.3f;

    public SwapingController(SwapingModel swapingModel,GridConfig config)
    {
        this.swapingModel = swapingModel;
        mainCam = Camera.main;
        this.config = config;
    }

    public void SwapingHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HadSwap = false;
            mouseDownPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
            currentCell = swapingModel.GetCellAtMousePosition(mouseDownPosition);
        }

        if (Input.GetMouseButton(0))
        {
            if (HadSwap || currentCell == new Vector2Int(999, 999)) return;
            Vector2 currentMousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(currentMousePosition, mouseDownPosition) > thresHold)
            {
                Vector2Int direction = GetDirection(currentMousePosition - mouseDownPosition);

                Vector2Int nextCell = currentCell + direction;
                
                if (nextCell.x < 0 || nextCell.x >= config.GridSize.x || nextCell.y < 0 ||
                    nextCell.y >= config.GridSize.y) return;
                
                swapingModel.Swap(currentCell.x, currentCell.y, nextCell.x, nextCell.y);
                HadSwap = true;
            }
        }
    }

    private Vector2Int GetDirection(Vector2 mousePostion)
    {
        if (Mathf.Abs(mousePostion.x) > Mathf.Abs(mousePostion.y))
        {
            return (mousePostion.x > 0) ? Vector2Int.up : Vector2Int.down;
        }

        return (mousePostion.y > 0) ? Vector2Int.right : Vector2Int.left;
    }
}