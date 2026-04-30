using System;
using System.Collections.Generic;
using UnityEngine;

public class SwapingModel
{
    private GridModel<ItemModel> grid;
    public event Action<List<int>, float> OnItemChange; 
    List<int> itemHolder = new List<int>();
    public SwapingModel(GridModel<ItemModel> grid)
    {
        this.grid = grid;
    }
    
    public void Swap(int x1,int y1,int x2,int y2)
    {
        itemHolder.Clear();
        ItemModel item1 = grid.GetCell(x1, y1);
        ItemModel item2 = grid.GetCell(x2, y2);
        itemHolder.Add(item1.ID);
        itemHolder.Add(item2.ID);
        grid.SwapTwoCell(x1,y1,x2,y2);
        item1.SetPosition(x2,y2); // set position in grid
        item2.SetPosition(x1,y1); // set position in grid
        OnItemChange?.Invoke(itemHolder,0.3f);
    }
    



}
