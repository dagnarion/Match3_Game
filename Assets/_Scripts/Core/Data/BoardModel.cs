using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;
public class BoardModel
{
    private GridConfig Config;
    private GridModel<ItemModel> grid;
    public event Action<ItemModel> OnItemCreate;
    public BoardModel(GridModel<ItemModel> grid, GridConfig config)
    {
        this.Config = config;
        this.grid = grid;
    }
    
    public void FillItemToBoard()
    {
        int xSize = Config.GridSize.x;
        int ySize = Config.GridSize.y;
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        {
            if (grid.GetCell(x, y) == null)
            {
                ItemModel item = ItemFactory.CreateRandomNormalItem(x, y);
                grid.SetCell(x, y, item);
                OnItemCreate?.Invoke(item);
            }
        }
    }
    
}
