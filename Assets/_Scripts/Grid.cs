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

    public void SwapItem(Vector2Int item1,Vector2Int item2)
    {
        Item _item1 = slots[item1.x, item1.y];
        Item _item2 = slots[item2.x, item2.y];
        if(_item1 == null || _item2 == null) return;
        Vector3 tmpPosition = _item1.transform.position;
        
        _item1.SetPosition(_item2.transform.position);
        _item2.SetPosition(tmpPosition);
        slots[item1.x, item1.y] = _item2;
        slots[item2.x, item2.y] = _item1;

    }

    public void RemoveASlot(int x,int y)
    {
        if(IsEmptySlot(x,y)) return;
        slots[x,y].gameObject.SetActive(false);
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
