using System;
using UnityEngine;

public class ItemModel
{
    private static int ItemID;
    public ItemType Type { get; private set; }
    public int x { get; private set; }
    public int y { get; private set; }
    public bool IsMatched { get; private set; }
    
    public int ID { get; private set; }
    public ItemModel(int x,int y,ItemType type)
    {
        this.x = x;
        this.y = y;
        this.Type = type;
        IsMatched = false;
        ID = ItemID++;
    }
    
    public void SetMatchState(bool condition) => IsMatched = condition;

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
}
