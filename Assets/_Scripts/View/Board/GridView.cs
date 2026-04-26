using System;
using UnityEngine;

public class GridView
{
    private GridConfig config;

    public GridView(GridConfig config)
    {
        this.config = config;
    }

    public void ChangeConfig(GridConfig config)
    {
        this.config = config;
    }
    
    public void Draw()
    {
        float xSize = config.GridSize.x;
        float ySize = config.GridSize.y;
        Vector2 gridInWorld = config.GetGridInWorld();
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Vector3 position = new Vector2(x, y) * config.CellSize + gridInWorld;
                Gizmos.DrawWireCube(position,config.CellSize);
            }
        }
    }
}
