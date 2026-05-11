using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchModel
{
    private GridConfig config;
    private GridModel<ItemModel> grid;
    public bool CanMatch { get; private set; }
    private MatchInfo[,] matchGrid;
    public MatchModel(GridModel<ItemModel> grid,GridConfig config)
    {
        this.grid = grid;
        this.config = config;
        matchGrid = new MatchInfo[config.GridSize.x, config.GridSize.y];
    }

    public void Match()
    {
        CanMatch = false;
        for (int row = 0; row < config.GridSize.x; row++)
            for (int col = 0; col < config.GridSize.y; col++)
            {
                matchGrid[row, col].Type = ItemType.None;
                matchGrid[row, col].x = row;
                matchGrid[row, col].y = col;
                matchGrid[row, col].h = 0;
                matchGrid[row, col].v = 0;
            }
        
        for (int row = 0; row < config.GridSize.x; row++)
        {
            int col = 0;
            while(col < config.GridSize.y)
            {
                int startPosition = col;
                ItemModel startModel = grid.GetCell(row, col);
                while (col < config.GridSize.y - 1 && startModel.Type == grid.GetCell(row, col + 1).Type)
                {
                    col++;
                }

                int length = col - startPosition + 1;
                if (length >= 3)
                {
                    for (int i = startPosition; i <= col; i++)
                    {
                        grid.GetCell(row,i).SetMatchState(true);
                        matchGrid[row, i].Type = startModel.Type;
                        matchGrid[row, i].h = length;
                    }
                    CanMatch = true;
                }

                col++;
            }
        }

        for (int col = 0; col < config.GridSize.y; col++)
        {
            int row = 0;
            while (row < config.GridSize.x)
            {
                int startPosition = row;
                ItemModel startModel = grid.GetCell(row, col);
                while (row + 1 < config.GridSize.x && startModel.Type == grid.GetCell(row + 1, col).Type)
                {
                    row++;
                }

                int length = row - startPosition + 1;
                if (length >= 3)
                {
                    for (int i = startPosition; i <= row; i++)
                    {
                        grid.GetCell(i,col).SetMatchState(true);
                        matchGrid[i, col].Type = startModel.Type;
                        matchGrid[i, col].v = length;
                    }
                    CanMatch = true;
                }
                row++;
            }
        }
    }

    public bool CheckMatchAt(int x,int y)
    {
            int col = 0;
            while(col < config.GridSize.y)
            {
                int startPosition = col;
                ItemModel startModel = grid.GetCell(x, col);
                while (col < config.GridSize.y - 1 && startModel.Type == grid.GetCell(x, col + 1).Type) col++;
                int length = col - startPosition + 1;
                if (length >= 3)
                {
                    return true;
                }
                col++;
            }
            
            int row = 0;
            while (row < config.GridSize.x)
            {
                int startPosition = row;
                ItemModel startModel = grid.GetCell(row, y);
                while (row + 1 < config.GridSize.x && startModel.Type == grid.GetCell(row + 1, y).Type)
                    row++;
                int length = row - startPosition + 1;
                if (length >= 3)
                {
                    return true;
                }
                row++;
            }

            return false;
    }

    private void Dfs(int x,int y,ItemType type,List<MatchInfo> shape,bool[,] visited)
    {
        if(x < 0 || x >= config.GridSize.x || y < 0 || y >= config.GridSize.y || matchGrid[x,y].Type == ItemType.None || matchGrid[x,y].Type != type || visited[x,y])
            return;
        visited[x, y] = true;
        shape.Add(matchGrid[x,y]);
        Dfs(x+1,y,type,shape,visited);
        Dfs(x-1,y,type,shape,visited);
        Dfs(x,y+1,type,shape,visited);
        Dfs(x,y-1,type,shape,visited);
    }

    private List<List<MatchInfo>> FloodFill()
    {
        List<List<MatchInfo>> shapeHolder = new List<List<MatchInfo>>();
        bool[,] visited = new bool[config.GridSize.x, config.GridSize.y];
        for(int row = 0;row < config.GridSize.x;row++)
        for (int col = 0; col < config.GridSize.y; col++)
        {
            ItemType type = matchGrid[row, col].Type;
            if (type != ItemType.None && !visited[row,col])
            {
                List<MatchInfo> shape = new List<MatchInfo>();
                Dfs(row,col,type,shape, visited);
                shapeHolder.Add(shape);
            }
        }

        return shapeHolder;
    }

    public List<(ItemType,ItemModifier,Vector2Int)> GetSpecialItem(Vector2Int swapedPosition)
    {
        List<MatchInfo> core = GetCoreItemInShape(swapedPosition);
        List<(ItemType,ItemModifier, Vector2Int)> itemHodler = new List<(ItemType,ItemModifier, Vector2Int)>();
        foreach (var it in core)
        {
            // if (it.h >= 5 || it.v >= 5)
            // {
            //     itemHodler.Add((it.Type,ItemModifier.Bomb,new Vector2Int(it.x,it.y)));
            //     continue;
            // }

            if (it.h >= 3 && it.v >= 3)
            {
                itemHodler.Add((it.Type,ItemModifier.Wrapped,new Vector2Int(it.x,it.y)));
                continue;
            }

            if (it.h == 4)
            {
                itemHodler.Add((it.Type,ItemModifier.VerticalStripped,new Vector2Int(it.x,it.y)));
                continue;
            }

            if (it.v == 4)
            {
                itemHodler.Add((it.Type,ItemModifier.HorizontalStripped,new Vector2Int(it.x,it.y)));
                continue;
            }
        }
        return itemHodler;
    }
    

    private List<MatchInfo> GetCoreItemInShape(Vector2Int swapedPosition)
    {
        List<MatchInfo> cores = new List<MatchInfo>();
        List<List<MatchInfo>> shapes = FloodFill();
        foreach (var shape in shapes)
        {
            MatchInfo core = new MatchInfo();
            int maxx = int.MinValue;
            foreach (var cell in shape)
            {
                if (maxx < cell.v + cell.h)
                {
                    maxx = cell.v + cell.h;
                    core = cell; // chỗ này nên xử lý là lấy ưu tiên kẹo người chơi swap
                }
            }

            if (core.h == 0 || core.v == 0)
            {
                shape.Sort((a, b) =>
                {
                    if (a.x != b.x) return a.x.CompareTo(b.x);
                    return a.y.CompareTo(b.y);
                });
                core = shape[shape.Count / 2];
            }

            foreach (var c in shape)
            {
                if (c.x == swapedPosition.x && c.y == swapedPosition.y)
                {
                    core = c;
                }
            }
            
            cores.Add(core);
        }
        return cores;
    }
    
}

public struct MatchInfo
{
    public ItemType Type;
    public int h;
    public int v;
    public int x;
    public int y;
}

// old Match
// public void Match()
// {
//     CanMatch = false;
//     for(int r = 0;r<config.GridSize.x;r++)
//     for (int c = 0; c < config.GridSize.y-2; c++)
//     {
//         ItemModel item1 = grid.GetCell(r, c);
//         ItemModel item2 = grid.GetCell(r, c+1);
//         ItemModel item3 = grid.GetCell(r, c+2);
//         if (item1 == null || item2 == null || item3 == null) continue;
//         if (item1.Type == item2.Type && item2.Type == item3.Type)
//         {
//                  CanMatch = true;
//                  item1.SetMatchState(true);
//                  item2.SetMatchState(true);
//                  item3.SetMatchState(true);
//         }
//     }
//     
//     for(int c = 0;c<config.GridSize.y;c++)
//     for (int r = 0; r < config.GridSize.x - 2; r++)
//     {
//         ItemModel item1 = grid.GetCell(r, c);
//         ItemModel item2 = grid.GetCell(r+1, c);
//         ItemModel item3 = grid.GetCell(r+2, c);
//         if (item1 == null || item2 == null || item3 == null) continue;
//         if (item1.Type == item2.Type && item2.Type == item3.Type)
//         {
//             CanMatch = true;
//             item1.SetMatchState(true);
//             item2.SetMatchState(true);
//             item3.SetMatchState(true);
//         }
//     }
// }