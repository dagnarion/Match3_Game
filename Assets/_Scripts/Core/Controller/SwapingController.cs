using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SwapingController
{
    private GridConfig config;
    private SwapingModel swapingModel;
    private WorldToGridConverter worldChange;
    private Vector2 mouseDownPosition;
    private Vector2Int currentCell;
    private Vector2Int nextCell;
    public bool HadSwap { get; private set; }
    private float thresHold = 0.3f;
    private Camera mainCam;


    public SwapingController(SwapingModel swapingModel, GridConfig config)
    {
        this.swapingModel = swapingModel;
        mainCam = Camera.main;
        this.config = config;
        worldChange = new WorldToGridConverter(config,Camera.main.transform.position);
    }

    public void SetUpHadSwap(bool condition) => HadSwap = condition;

    public void SwapingHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //HadSwap = false;
            mouseDownPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
            currentCell = worldChange.ConvertWorldPositionToGridCell(mouseDownPosition);
        }

        if (Input.GetMouseButton(0))
        {
            if (HadSwap || currentCell.x<0 || currentCell.y<0 || currentCell.x >= config.GridSize.x || currentCell.y >= config.GridSize.y) return;
            Vector2 currentMousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(currentMousePosition, mouseDownPosition) > thresHold)
            {
                Vector2Int direction = GetDirection(currentMousePosition - mouseDownPosition);

                nextCell = currentCell + direction;

                if (nextCell.x < 0 || nextCell.x >= config.GridSize.x || nextCell.y < 0 ||
                    nextCell.y >= config.GridSize.y) return;

                swapingModel.Swap(currentCell.x, currentCell.y, nextCell.x, nextCell.y);
                HadSwap = true;
            }
        }
    }

    public void SwapBack()
    {
        swapingModel.Swap(currentCell.x, currentCell.y, nextCell.x, nextCell.y);

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