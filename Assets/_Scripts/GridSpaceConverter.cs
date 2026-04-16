using UnityEngine;

public class GridSpaceConverter
{
    private GridContext gridContext;

    public GridSpaceConverter(GridContext context)
    {
        gridContext = context;
    }
    
    public void ChangeGridContext(GridContext context)
    {
        gridContext = context;
    }
    
    void GetXYInScreen(Vector2 worldPos , out int x, out int y)
    {
        Vector2 cellOffSet = gridContext.cellSize * 0.5f;
        Vector2 originPosition = gridContext.GetGridInWorld();
        x = Mathf.FloorToInt(((worldPos - originPosition).x + cellOffSet.x) / gridContext.cellSize.x);
        y = Mathf.FloorToInt(((worldPos - originPosition).y + cellOffSet.y) / gridContext.cellSize.y);
    }

    public Vector2Int GetCell(Vector2 worldPos)
    {
        int x, y;
        GetXYInScreen(worldPos,out x,out y);
        if (x < 0 || y < 0 || x >= gridContext .gridSize.x || y >= gridContext.gridSize.y) return new Vector2Int(9999,9999);
        return new Vector2Int(x, y);
    }

}
