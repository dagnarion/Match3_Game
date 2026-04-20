using UnityEngine;

public class Grid 
{
    private Vector2Int gridSize;
    private Item[,] slots;
    public Grid(Vector2Int gridSize)
    {
        this.gridSize = gridSize;
        slots = new Item[gridSize.x,gridSize.y];
    }
    
    public void AddItemToSlot(Item item,int x,int y)
    {
        if(!IsEmptySlot(x,y)) return;
        slots[x, y] = item;
    }

    public Item GetItemFromSlot(int x, int y)
    {
        if (IsEmptySlot(x, y))
        {
            Debug.LogError("There Was Not Item In Slot");
            return null;
        }
        return slots[x, y];
    }

    public void RemoveASlot(int x,int y)
    {
        if(IsEmptySlot(x,y)) return;
        slots[x,y].gameObject.SetActive(false);
        slots[x, y] = null;
    }

    public bool IsEmptySlot(int x, int y)
    {           
        if (slots[x, y] == null)
        {
            return true;
        }
        return slots[x, y].gameObject.activeSelf == false;
    }
}
