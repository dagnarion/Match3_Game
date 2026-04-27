using System;
using UnityEngine;

public class ItemModel
{
    public ItemType Type { get; private set; }
    public int x { get; private set; }
    public int y { get; private set; }
    public bool IsMatched { get; private set; }
    public event Action Matched;
    public event Action<int,int,float> PositionChange;
    public ItemModel(int x,int y,ItemType type)
    {
        this.x = x;
        this.y = y;
        this.Type = type;
        IsMatched = false;
    }

    public void DisableItem() => Matched?.Invoke();

    public void SetMatchState(bool condition) => IsMatched = condition;

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
        PositionChange?.Invoke(x,y,0.2f);
    }
    
}
