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
    private ItemType[] values;
    private int Range;
    public BoardModel(GridModel<ItemModel> grid, GridConfig config)
    {
        this.Config = config;
        this.grid = grid;
        values = (ItemType[])Enum.GetValues(typeof(ItemType));
        ItemType temp  = (ItemType)Enum.GetValues(typeof(ItemType)).Cast<ItemType>().Min();
        Range = Array.IndexOf(values, temp);
    }
    
    public void InitializeBoard()
    {
        int xSize = Config.GridSize.x;
        int ySize = Config.GridSize.y;
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        {
            ItemModel item = new ItemModel(x, y, RandomType());
            grid.SetCell(x, y, item);
            OnItemCreate?.Invoke(item);
        }
    }
    
    private ItemType RandomType()
    {
        int randomIndex = UnityEngine.Random.Range(1, Range);
        return values[randomIndex];
    }
    
}
