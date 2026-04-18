using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [Header("REFERENCE")] 
    [SerializeField] private ItemSO item;
    [SerializeField] private GridContext context;
    private GridSpaceConverter gridSpaceConverter;
    private Grid<ItemSO> slots;
    private GridDraw gridDraw;
    
    private Camera mainCam;
    
    
    private void Awake()
    {
        slots = new Grid<ItemSO>(context.gridSize);
        gridDraw = new GridDraw(context);
        gridSpaceConverter = new GridSpaceConverter(context);
        mainCam = Camera.main;
    }

    private void Start()
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

    private void InitializeItem(Vector2 position)
    {
        Vector2Int itemPosition =  gridSpaceConverter.GetCell(position);
        slots.AddItemToSlot(item,itemPosition.x,itemPosition.y);
        ItemFactory.CreateItem(item).transform.position = position;
    }
    
    public void OnDrawGizmos()
    {
        if(gridDraw == null) return;
        gridDraw.ChangeGridContext(context);
        gridDraw.Draw();
    }
    
}
