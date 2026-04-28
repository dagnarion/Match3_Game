using UnityEngine;

public class WorldToGridConverter
{
    private GridConfig config;
    private Vector2 originPosition;
    private Vector2 cellOffSet;

    public WorldToGridConverter(GridConfig config,Vector2 gridStartPosition)
    {
        this.config = config;
        cellOffSet = 0.5f * config.CellSize;
        Vector2 offSet = new Vector2(-0.5f * config.CellSize.x * (config.GridSize.x - 1), -0.5f * config.CellSize.y * (config.GridSize.y - 1));
        originPosition = gridStartPosition + offSet;
    }
    
    public Vector2Int ConvertWorldPositionToGridCell(Vector2 worldPos)
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
        return new Vector2Int(x, y);
    }
}
