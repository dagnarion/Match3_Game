using UnityEngine;

public class GravityModel
{
    private GridConfig config;
    private GridModel<ItemModel> grid;
    
    public GravityModel(GridModel<ItemModel> grid,GridConfig config)
    {
        this.grid = grid;
        this.config = config;
    }

    public void ApplyGravity()
    {
        int rows = config.GridSize.x;
        int cols = config.GridSize.y;
        for (int c = 0; c < cols; c++)
        {
            int idx = 0;
        
            for (int r = 0; r <rows; r++)
            {
                if (grid.GetCell(r,c) != null)
                {
                    grid.SetCell(idx, c, grid.GetCell(r, c));
                    grid.GetCell(idx, c).SetPosition(r, c);
                    idx++;
                }
            }
        
            for (int r = idx; r <rows; r++)
            {
                grid.ClearACell(r,c);
            }
        }
    }
    
    
}
