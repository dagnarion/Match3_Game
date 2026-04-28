using System;
using UnityEngine;

public class SwapingModel
{
    private GridModel<ItemModel> grid;
    
    public SwapingModel(GridModel<ItemModel> grid)
    {
        this.grid = grid;
    }
    
    public void Swap(int x1,int y1,int x2,int y2)
    {
        ItemModel item1 = grid.GetCell(x1, y1);
        ItemModel item2 = grid.GetCell(x2, y2);
        grid.SwapTwoCell(x1,y1,x2,y2);
        item1.SetPosition(x2,y2);
        item2.SetPosition(x1,y1);
    }
    



}
