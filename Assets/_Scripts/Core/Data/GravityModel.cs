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
        // int rows = grid.GetLength(0);
        // int cols = grid.GetLength(1);
        // for (int c = 0; c < cols; c++)
        // {
        //     int idx = rows - 1;
        //
        //     for (int r = rows - 1; r >= 0; r--)
        //     {
        //         if (grid[r, c] > 0)
        //         {
        //             grid[idx, c] = grid[r, c];
        //             idx--;
        //         }
        //     }
        //
        //     for (int r = idx; r >= 0; r--)
        //     {
        //         grid[r, c] = 0;
        //     }
        // }
    }
    
    
}
