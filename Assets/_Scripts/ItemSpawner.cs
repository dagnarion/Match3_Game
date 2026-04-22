using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private Transform gemHolder;
    [SerializeField] private GridContext context;
    [SerializeField] private List<ItemSO> item;
    private GridSpaceConverter gridSpaceConverter;
    private Grid slots;
    
    private void Start()
    {
        slots = _gameBoard.slots;
        gridSpaceConverter = new GridSpaceConverter(context);
        StartFillItemToBoard();
    }

    public void StartFillItemToBoard()
    {
        FillItemToBoard();
    }
    
    private void FillItemToBoard()
    {
        float xSize = context.gridSize.x;
        float ySize = context.gridSize.y;
        Vector2 gridInWorld = context.GetGridInWorld();
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Vector3 position = new Vector2(x, y) * context.cellSize + gridInWorld;
                InitializeItem(position);
            }
        }
    }
    
    public void ClearFullItem()
    {
        float xSize = context.gridSize.x;
        float ySize = context.gridSize.y;
        Vector2 gridInWorld = context.GetGridInWorld();
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Vector3 position = new Vector2(x, y) * context.cellSize + gridInWorld;
                ClearASlot(position);
            }
        }
    }
    
    private void InitializeItem(Vector2 position)
    {
        Vector2Int itemPosition = gridSpaceConverter.GetCell(position);
        if (!slots.IsEmptySlot(itemPosition.x, itemPosition.y)) return;
        
        ItemSO randItem = item[Random.Range(0, item.Count)];
        GameObject items = ItemFactory.CreateItem(randItem,position,gemHolder);

        slots.AddItemToSlot(items.GetComponent<Item>(), itemPosition.x, itemPosition.y);
    }
    
    private void ClearASlot(Vector2 position)
    {
        Vector2Int itemPosition = gridSpaceConverter.GetCell(position);
        if (slots == null || slots.IsEmptySlot(itemPosition.x, itemPosition.y)) return;
        slots.RemoveASlot(itemPosition.x, itemPosition.y);
        // SlotCheck[itemPosition.x, itemPosition.y] = false;
    }
}
