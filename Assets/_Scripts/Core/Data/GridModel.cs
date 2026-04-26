using System.Collections.Generic;
using UnityEngine;

public class GridModel<T>
{
    private T[,] grid;

    public GridModel(int xSize,int ySize)
    {
        grid = new T[xSize, ySize];
    }

    public void SwapTwoCell(int x1,int y1,int x2,int y2)
    {
        if (!IsValidPosition(x1, y1) || !IsValidPosition(x2,y2)) return;

        T temp = grid[x1, y1];
        grid[x1, y1] = grid[x2, y2];
        grid[x2, y2] = temp;

    }

    public void SetCell(int x,int y,T value)
    {
        if(!IsValidPosition(x,y)) return;
     //   if(!IsEmptyCell(x,y)) return;
        grid[x, y] = value;
    }

    public T GetCell(int x,int y)
    {
        if(!IsValidPosition(x,y)) return default;
        if(IsEmptyCell(x,y)) return default;
        return grid[x, y];
    }

    public void ClearACell(int x,int y)
    {
        if(!IsValidPosition(x,y)) return;
        if(IsEmptyCell(x,y)) return;
        grid[x, y] = default;
    }

    public void ClearGrid()
    {
        for(int x = 0;x<grid.GetLength(0);x++)
            for(int y = 0;y<grid.GetLength(1);y++)
                ClearACell(x,y);
    }
    
    public bool IsEmptyCell(int x,int y)
    {
        if(!IsValidPosition(x,y))
        {
            Debug.LogError("This Position Is Not In Grid");
            return false;
        }
     return EqualityComparer<T>.Default.Equals(grid[x,y], default(T));
    }

    private bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1);
    }
}


