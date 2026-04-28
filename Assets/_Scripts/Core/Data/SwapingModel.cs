using System;
using UnityEngine;

public class SwapingModel
{
    private GridConfig config;
    private GridModel<ItemModel> grid;
    private Vector2 originPosition;
    private Vector2 cellOffSet;
    
    public SwapingModel(GridModel<ItemModel> grid,GridConfig config)
    {
        this.config = config;
        this.grid = grid;
        cellOffSet = 0.5f * config.CellSize;
        Vector2 offSet = new Vector2(-0.5f * config.CellSize.x * (config.GridSize.x - 1), -0.5f * config.CellSize.y * (config.GridSize.y - 1));
        originPosition = (Vector2)Camera.main.transform.position + offSet;
    }
    
    public void Swap(int x1,int y1,int x2,int y2)
    {
        ItemModel item1 = grid.GetCell(x1, y1);
        ItemModel item2 = grid.GetCell(x2, y2);
        grid.SwapTwoCell(x1,y1,x2,y2);
        item1.SetPosition(x2,y2);
        item2.SetPosition(x1,y1);
    }
    
    public Vector2Int GetCellAtMousePosition(Vector2 worldPos)
    {
        int x, y;
        GetXYInScreen(worldPos, out x, out y);
        return GetCell(y, x);
    }
    
    private void GetXYInScreen(Vector2 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt(((worldPos - originPosition).x + cellOffSet.x) / config.CellSize.x);
        y = Mathf.FloorToInt(((worldPos - originPosition).y + cellOffSet.y) / config.CellSize.y);
    }
    
    private Vector2Int GetCell(int x, int y)
    {
        if (x < 0 || y < 0 || x >= config.GridSize.x || y >= config.GridSize.y) return new Vector2Int(999,999);
        return new Vector2Int(x, y);
    }


}
