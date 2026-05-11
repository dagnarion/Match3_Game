using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class BoardView : MonoBehaviour
{
    [SerializeField] private Transform itemHolder;
    [SerializeField] private List<ItemView> itemPrefabs = new List<ItemView>();
    private Dictionary<(ItemType,ItemModifier), ItemView> items = new Dictionary<(ItemType,ItemModifier), ItemView>();
    private GridConfig config;
    private Camera mainCam;
    private WorldToGridConverter converter;
    private int YOutCameraValue;
    private Dictionary<int, ItemView> ItemDictionary = new Dictionary<int, ItemView>();
    public bool CompleteTransition { get; private set; }
    private Sequence currentSq;
    private void Awake()
    {
        mainCam = Camera.main;
        converter = new WorldToGridConverter(config, mainCam.transform.position);
        YOutCameraValue = GetOutSightCamera();
        foreach (ItemView item in itemPrefabs)
        {
            if (items.ContainsKey((item.Type,item.Modityfier)) )continue;
            items.Add((item.Type,item.Modityfier), item);
        }
    }
    
    public void Init(GridConfig config)
    {
        this.config = config;
    }
    
    public void CreateItemToSpawnPositionOnBoard(ItemModel itemModel)
    {
        if (!items.ContainsKey((itemModel.Type,itemModel.Modifier)))
        {
            Debug.LogError("khong co item trong list");
            return;
        }

        ItemView item = Instantiate<ItemView>(items[(itemModel.Type,itemModel.Modifier)], itemHolder); // object pooling
        item.SetItemModel(itemModel);
        item.SetGridConfig(config);
        item.SetStartPosition(YOutCameraValue,itemModel.x);
        if (!ItemDictionary.ContainsKey(itemModel.ID)) ItemDictionary.Add(itemModel.ID, item);
        else ItemDictionary[itemModel.ID] = item;
    }

    public void CreateItemToSpecificPositionOnBoard(ItemModel itemModel)
    {
        if (!items.ContainsKey((itemModel.Type,itemModel.Modifier)))
        {
            Debug.LogError("khong co item trong list");
            return;
        }

        ItemView item = Instantiate<ItemView>(items[(itemModel.Type,itemModel.Modifier)], itemHolder); 
        item.SetItemModel(itemModel);
        item.SetGridConfig(config);
        item.SetStartPosition(itemModel.x,itemModel.y);
        if (!ItemDictionary.ContainsKey(itemModel.ID)) ItemDictionary.Add(itemModel.ID, item);
        else ItemDictionary[itemModel.ID] = item;
    }

    public void DoItemsAnimation(List<int> ItemIDs,float duration)
    {
        if(ItemIDs.Count == 0) {CompleteTransition = true; return;}
        CompleteTransition = false;
        currentSq?.Kill();
        currentSq = DOTween.Sequence();
        foreach (int ID in ItemIDs)
        {
            if (ItemDictionary.TryGetValue(ID,out var item) && item!=null)
            {
                currentSq.Join(item.DoTransition(duration));
            }
        }
        currentSq.OnComplete(() => { CompleteTransition = true; });
    }

    public void RemoveItemOnCell(int ID)
    {
        if (ItemDictionary.TryGetValue(ID, out var item) && item != null)
        {
            item.ReleaseItem();
            ItemDictionary.Remove(ID);
        }
    }    
    
    private int GetOutSightCamera()
    {
        Vector3 OutCameraPosition = mainCam.ViewportToWorldPoint(new Vector3(1f, 0.5f, 10f)) + new Vector3(1.5f,0,0); //bug nma tu nhien ra hieu ung dep phet =)))
        Vector2Int pos = converter.ConvertWorldPositionToGridCell((Vector2)OutCameraPosition);
        return pos.y;
    }

}