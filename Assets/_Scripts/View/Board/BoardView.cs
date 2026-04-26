using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    
    [SerializeField] private Transform itemHolder;
    [SerializeField] private List<ItemView> itemPrefabs = new List<ItemView>();
    private Dictionary<ItemType, ItemView> items = new Dictionary<ItemType, ItemView>(); 
    
    private void Awake()
    {
        foreach (ItemView item in itemPrefabs)
        {
            if(items.ContainsKey(item.Type)) continue;
            items.Add(item.Type,item);
        }
    }
    
    public void CreateItemOnBoard(ItemModel itemModel,GridConfig config)
    {
        if (!items.ContainsKey(itemModel.Type))
        {
            Debug.LogError("khong co item trong list");
            return;
        }
        ItemView item = Instantiate<ItemView>(items[itemModel.Type],itemHolder);
        item.SetUp(itemModel,config);
    }
    
}
