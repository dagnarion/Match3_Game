using UnityEngine;

public class HintModel
{
    private GridConfig config;
    private GridModel<ItemModel> grid;
    private MatchModel matchModel;

    public HintModel(GridModel<ItemModel> grid,GridConfig config,MatchModel matchModel)
    {
        this.config = config;
        this.grid = grid;
        this.matchModel = matchModel;
    }
    
    private void CheckPosibleMove(ref Vector2Int currentItem)
    {
        for(int row = 0;row < config.GridSize.x;row++)
        for (int col = 0; col < config.GridSize.y; col++)
        {
            currentItem = new Vector2Int(row, col);
            foreach (var direction in DirectionModel.Direction)
            {
                Vector2Int nextItem = currentItem + direction;
                if(nextItem.x < 0 || nextItem.x >= config.GridSize.x || nextItem.y < 0 || nextItem.y >= config.GridSize.y) continue;
                grid.SwapTwoCell(currentItem.x,currentItem.y,nextItem.x,nextItem.y);
                matchModel.Match();
                grid.SwapTwoCell(currentItem.x,currentItem.y,nextItem.x,nextItem.y);
                if(matchModel.CanMatch) return;
            }
        }
        currentItem = new Vector2Int(-1, -1);
        return;
    }

    public Vector2Int GetPosibleMove()
    {
        Vector2Int currentItem = Vector2Int.zero;
        CheckPosibleMove(ref currentItem);
        return currentItem;
    }
}
