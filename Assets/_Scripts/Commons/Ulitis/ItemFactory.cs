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
        ItemModel model = CreateItem(RandomType(), x, y);
        return model;
    }

    public static ItemModel CreateItem(ItemType type,int x,int y)
    {
        ItemModel model = new ItemModel(x, y, type);
        return model;
    }
    
    private static ItemType RandomType()
    {
        int randomIndex = UnityEngine.Random.Range(1, 5); // test
        return values[randomIndex];
    }
    
    
}
