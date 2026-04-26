using System.Runtime.InteropServices;
using UnityEngine;

public class ItemModel
{
    public int x;
    public int y;
    public ItemType Type;

    public ItemModel(int x,int y,ItemType type)
    {
        this.x = x;
        this.y = y;
        this.Type = type;
    }

    public void SwapPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SwapType(ItemType type)
    {
        this.Type = type;
    }

    public void SwapModel(ItemModel item)
    {
        int tempX = x, tempY = y;
        ItemType type = Type;
        SwapPosition(item.x, item.y);
        SwapType(item.Type);

        item.SwapPosition(tempX, tempY);
        item.SwapType(type);
    }
}


