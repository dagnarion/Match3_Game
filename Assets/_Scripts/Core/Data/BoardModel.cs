using System;
using System.Collections.Generic;
using UnityEngine;

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
        Array values = Enum.GetValues(typeof(ItemType));
        int randomIndex = UnityEngine.Random.Range(1, values.Length);
        return (ItemType)values.GetValue(randomIndex);
    }
    
}
