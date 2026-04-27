using UnityEngine;

public class MatchModel
{
    private GridConfig config;
    private GridModel<ItemModel> grid;
    private bool IsCompleteMatching;
    public MatchModel(GridModel<ItemModel> grid,GridConfig config)
    {
        this.grid = grid;
        this.config = config;
    }

    public void Match()
    {
        // check row
        IsCompleteMatching = true;
        for(int r = 0;r<config.GridSize.x;r++)
        for (int c = 0; c < config.GridSize.y-2; c++)
        {
            int cell1 = Mathf.Abs((int)grid.GetCell(r, c).Type);
            int cell2 = Mathf.Abs((int)grid.GetCell(r, c+1).Type);
            int cell3 = Mathf.Abs((int)grid.GetCell(r, c+2).Type);
            if (cell1 == cell2 && cell2 == cell3 && cell1 != 0)
            {
                grid.GetCell(r,c).SwapType((ItemType) (-cell1));
                grid.GetCell(r,c+1).SwapType((ItemType) (-cell1));
                grid.GetCell(r,c+2).SwapType((ItemType) (-cell1));
                IsCompleteMatching = false;
            }
        }
        
        for(int c = 0;c<config.GridSize.y;c++)
        for (int r = 0; r < config.GridSize.x - 2; r++)
        {
            int cell1 = Mathf.Abs((int)grid.GetCell(r, c).Type);
            int cell2 = Mathf.Abs((int)grid.GetCell(r+1, c).Type);
            int cell3 = Mathf.Abs((int)grid.GetCell(r+2, c).Type);
            if (cell1 == cell2 && cell2 == cell3 && cell1 != 0)
            {
                grid.GetCell(r,c).SwapType((ItemType) (-cell1));
                grid.GetCell(r+1,c).SwapType((ItemType) (-cell1));
                grid.GetCell(r+2,c).SwapType((ItemType) (-cell1));
                IsCompleteMatching = false;
            }
        }
    }

    public bool IsComplete() => IsCompleteMatching;
}
