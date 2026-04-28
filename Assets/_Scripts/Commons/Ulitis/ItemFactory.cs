using System;
using System.Linq;
using UnityEngine;

public static class ItemFactory
{
    private static ItemType[] values = (ItemType[])Enum.GetValues(typeof(ItemType));
    private static ItemType temp  = (ItemType)Enum.GetValues(typeof(ItemType)).Cast<ItemType>().Min();
    private static int Range = Array.IndexOf(values, temp);
    public static ItemModel CreateRandomNormalItem(int x,int y)
    {
        ItemModel model = new ItemModel(x, y, RandomType());
        return model;
    }

    public static ItemModel CreateSpecialItem(int x,int y)
    {
        return null;
    }
    
    private static ItemType RandomType()
    {
        int randomIndex = UnityEngine.Random.Range(1, Range);
        return values[randomIndex];
    }
    
    
}
