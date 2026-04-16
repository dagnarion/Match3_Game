using UnityEngine;

public class GridSpaceConverter
{
    private Vector2Int gridSize;
    private Vector2 cellSize;
    private Vector2 originPosition;

    public GridSpaceConverter(GridContext context)
    {
        cellSize = context.cellSize;
        originPosition = context.GetGridInWorld();
        gridSize = context.gridSize;
    }
    
    public void ChangeGridContext(GridContext context)
    {
        cellSize = context.cellSize;
        originPosition = context.GetGridInWorld();
        gridSize = context.gridSize;
    }
    
    void GetXYInScreen(Vector2 worldPos , out int x, out int y)
    {
        Vector2 cellOffSet = cellSize * 0.5f;
        x = Mathf.FloorToInt(((worldPos - originPosition).x + cellOffSet.x) / cellSize.x);
        y = Mathf.FloorToInt(((worldPos - originPosition).y + cellOffSet.y) / cellSize.y);
    }

    public Vector2Int GetCell(Vector2 worldPos)
    {
        int x, y;
        GetXYInScreen(worldPos,out x,out y);
        return new Vector2Int(x, y);
    }

}
