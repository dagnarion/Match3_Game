using System;
using Random = UnityEngine.Random;

public class ReshuffleModel
{
    private GridConfig config;
    private GridModel<ItemModel> grid;
    public event Action<ItemModel> OnReshuffle;
    public ReshuffleModel(GridModel<ItemModel> grid, GridConfig config)
    {
        this.grid = grid;
        this.config = config;
    }

    public void Reshuffle()
    {
        for(int row = 0;row<config.GridSize.x;row++)
        for (int col = 0; col < config.GridSize.y; col++)
        {
            int ranX = Random.Range(row, config.GridSize.x);
            int ranY = Random.Range(col, config.GridSize.y);
            grid.SwapTwoCell(row,col,ranX,ranY);
        }        
        
        for(int row = 0;row<config.GridSize.x;row++)
        for (int col = 0; col < config.GridSize.y; col++)
        {
           ItemModel item = grid.GetCell(row, col);
           OnReshuffle?.Invoke(item);
        }
    }
}
