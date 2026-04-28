using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private Transform itemHolder;
    [SerializeField] private List<ItemView> itemPrefabs = new List<ItemView>();
    private Dictionary<ItemType, ItemView> items = new Dictionary<ItemType, ItemView>();
    private GridConfig config;
    private Camera mainCam;
    private WorldToGridConverter converter;
    private int YOutCameraValue;
    private void Awake()
    {
        mainCam = Camera.main;
        converter = new WorldToGridConverter(config, mainCam.transform.position);
        YOutCameraValue = GetOutSightCamera();
        foreach (ItemView item in itemPrefabs)
        {
            if (items.ContainsKey(item.Type)) continue;
            items.Add(item.Type, item);
        }
    }
    
    public void Init(GridConfig config)
    {
        this.config = config;
    }
    
    public void CreateFillItemToBoard(ItemModel itemModel)
    {
        if (!items.ContainsKey(itemModel.Type))
        {
            Debug.LogError("khong co item trong list");
            return;
        }

        ItemView item = Instantiate<ItemView>(items[itemModel.Type], itemHolder);
        item.SetUp(itemModel, config);
        item.SetStartPosition(YOutCameraValue,itemModel.x); // bug nma tu nhien ra hieu ung dep phet =)))
    }

    private int GetOutSightCamera()
    {
        Vector3 OutCameraPosition = mainCam.ViewportToWorldPoint(new Vector3(1f, 0.5f, 10f)) + new Vector3(1.5f,0,0); //bug nma tu nhien ra hieu ung dep phet =)))
        Vector2Int pos = converter.ConvertWorldPositionToGridCell((Vector2)OutCameraPosition);
        return pos.y;
    }

}