using System;
using NUnit.Framework;
using UnityEngine;

public class MatchModel
{
    private GridConfig config;
    private GridModel<ItemModel> grid;
    public bool CanMatch { get; private set; }
    public MatchModel(GridModel<ItemModel> grid,GridConfig config)
    {
        this.grid = grid;
        this.config = config;
    }

    public void Match()
    {
        CanMatch = false;
        for(int r = 0;r<config.GridSize.x;r++)
        for (int c = 0; c < config.GridSize.y-2; c++)
        {
            ItemModel item1 = grid.GetCell(r, c);
            ItemModel item2 = grid.GetCell(r, c+1);
            ItemModel item3 = grid.GetCell(r, c+2);
            if (item1 == null || item2 == null || item3 == null) continue;
            if (item1.Type == item2.Type && item2.Type == item3.Type)
            {
                     CanMatch = true;
                     item1.SetMatchState(true);
                     item2.SetMatchState(true);
                     item3.SetMatchState(true);
            }
        }
        
        for(int c = 0;c<config.GridSize.y;c++)
        for (int r = 0; r < config.GridSize.x - 2; r++)
        {
            ItemModel item1 = grid.GetCell(r, c);
            ItemModel item2 = grid.GetCell(r+1, c);
            ItemModel item3 = grid.GetCell(r+2, c);
            if (item1 == null || item2 == null || item3 == null) continue;
            if (item1.Type == item2.Type && item2.Type == item3.Type)
            {
                CanMatch = true;
                item1.SetMatchState(true);
                item2.SetMatchState(true);
                item3.SetMatchState(true);
            }
        }
    }
}
