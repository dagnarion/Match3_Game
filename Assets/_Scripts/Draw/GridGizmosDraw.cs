using UnityEngine;

public class GridGizmosDraw : IDraw
{
    private Vector2 cellSize;
    private Vector2 gridPosition;
    private Vector2 gridSize;
    public GridGizmosDraw(GridContext context)
    {
        cellSize = context.cellSize;
        gridPosition = context.GetGridInWorld();
        gridSize = context.gridSize;
    }

    public void ChangeGridContext(GridContext context)
    {
        cellSize = context.cellSize;
        gridPosition = context.GetGridInWorld();
        gridSize = context.gridSize;
    }

    public void Draw()
    {
        float xSize = gridSize.x;
        float ySize = gridSize.y;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Vector3 position = new Vector2(x, y) * cellSize + gridPosition;
                Gizmos.DrawWireCube(position,cellSize);
            }
        }
    }
}
