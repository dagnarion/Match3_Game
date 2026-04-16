using UnityEngine;

public class GridGameViewDraw : IDraw
{
    private Vector2 cellSize;
    private Vector2 gridPosition;
    private Vector2 gridSize;
    private LineRenderer gridline;
    public GridGameViewDraw(GridContext context,LineRenderer line)
    {
        cellSize = context.cellSize;
        gridPosition = context.GetGridInWorld();
        gridSize = context.gridSize;
        gridline = line;
    }

    public void ChangeGridContext(GridContext context)
    {
        cellSize = context.cellSize;
        gridPosition = context.GetGridInWorld();
        gridSize = context.gridSize;
    }

    public void Draw()
    {

    }
}

