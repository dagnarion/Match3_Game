using System;
using System.Collections.Generic;
using UnityEngine;

public class GravityModel
{
    private GridConfig config;
    private GridModel<ItemModel> grid;
    public event Action<List<int>,float> OnItemChange;
    List<int> itemHolder = new List<int>();
    public GravityModel(GridModel<ItemModel> grid,GridConfig config)
    {
        this.grid = grid;
        this.config = config;
    }

    public void ApplyGravity()
    {
        int rows = config.GridSize.x;
        int cols = config.GridSize.y;
        itemHolder.Clear();
        for (int c = 0; c < cols; c++)
        {
            int idx = 0;
        
            for (int r = 0; r <rows; r++)
            {
                if (grid.GetCell(r,c) != null )
                {
                        grid.SetCell(idx, c, grid.GetCell(r, c));
                        grid.GetCell(idx, c).SetPosition(idx, c); // set position in grid 
                        itemHolder.Add(grid.GetCell(idx,c).ID);
                    idx++;
                }
            }
        
            for (int r = idx; r <rows; r++)
            {
                grid.ClearACell(r,c);
            }
        }
        OnItemChange?.Invoke(itemHolder,0.6f);
    }
    
    
}
