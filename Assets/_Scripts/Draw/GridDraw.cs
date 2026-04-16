using UnityEngine;

public class GridDraw
{
    private GridContext gridContext;
    public GridDraw(GridContext context)
    {
        gridContext = context;
    }

    public void ChangeGridContext(GridContext context)
    {
        gridContext = context;
    }

    public void Draw()
    {
        float xSize = gridContext.gridSize.x;
        float ySize = gridContext.gridSize.y;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Vector3 position = new Vector2(x, y) * gridContext.cellSize + gridContext.GetGridInWorld();
                Gizmos.DrawWireCube(position,gridContext.cellSize);
            }
        }
    }
}
