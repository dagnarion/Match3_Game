using UnityEngine;

public class Grid<T> where T : class
{
    private Vector2Int gridSize;
    private T[,] slots;
    public Grid(Vector2Int gridSize)
    {
        this.gridSize = gridSize;
        slots = new T[gridSize.x,gridSize.y];
    }
    
    public void AddItemToSlot(T item,int x,int y)
    {
        if(!IsEmptySlot(x,y)) return;
        slots[x, y] = item;
    }

    public T GetItemFromSlot(int x, int y)
    {
        if (IsEmptySlot(x, y))
        {
            Debug.LogError("There Was Not Item In Slot");
            return null;
        }
        return slots[x, y];
    }

    public void ClearASlot(int x,int y)
    {
        if(IsEmptySlot(x,y)) return;
        slots[x, y] = null;
    }
    
    public bool IsEmptySlot(int x, int y) => slots[x, y] == null;
}
