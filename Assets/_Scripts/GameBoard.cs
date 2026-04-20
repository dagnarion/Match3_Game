using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameBoard : MonoBehaviour
{
    [Header("REFERENCE")] 
    [SerializeField] private Transform gemHolder;
    [SerializeField] private List<ItemSO> item;
    [SerializeField] private GridContext context;
    private GridSpaceConverter gridSpaceConverter;
    private Grid slots;
    private GridDraw gridDraw;
    private Camera mainCam;
    private bool[,] SlotCheck;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        slots = new Grid(context.gridSize);
        gridDraw = new GridDraw(context);
        gridSpaceConverter = new GridSpaceConverter(context);
        SlotCheck = new bool[context.gridSize.x, context.gridSize.y];
        mainCam = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int cell = gridSpaceConverter.GetCell(mousePosition);
            if (cell != new Vector2Int(9999, 9999))
            {
                slots.RemoveASlot(cell.x, cell.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
           MatchAllBoard();
        }
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
        SlotCheck[itemPosition.x, itemPosition.y] = false;
    }

    private void MatchAllBoard()
    {
        for(int i = 0;i<context.gridSize.x;i++)
        for (int j = 0; j < context.gridSize.y; j++)
        {
            Match(i, j);
        }
    }
    
    private void Match(int x,int y)
    {
        Array.Clear(SlotCheck,0,SlotCheck.Length);
        if(slots.IsEmptySlot(x,y)) return;
        Vector2Int rightCheck = new Vector2Int(x, y);
        Vector2Int leftCheck = new Vector2Int(x, y);
        Vector2Int upCheck = new Vector2Int(x, y);
        Vector2Int downCheck = new Vector2Int(x, y);
        ItemConfig currentItem = slots.GetItemFromSlot(x, y).GetItemType();

        int horizontalCnt = 1, vertiaclCnt = 1;
        
        SlotCheck[x, y] = true;
        while (rightCheck.x < context.gridSize.x && !slots.IsEmptySlot(rightCheck.x,rightCheck.y) && currentItem == slots.GetItemFromSlot(rightCheck.x, rightCheck.y).GetItemType())
        {
            rightCheck += Vector2Int.right;
            if (rightCheck.x < context.gridSize.x && !slots.IsEmptySlot(rightCheck.x,rightCheck.y) &&
                currentItem == slots.GetItemFromSlot(rightCheck.x, rightCheck.y).GetItemType())
            {
                SlotCheck[rightCheck.x, rightCheck.y] = true;
                horizontalCnt++;
            }
        }    
        
        while (leftCheck.x >= 0&& !slots.IsEmptySlot(leftCheck.x,leftCheck.y) && currentItem == slots.GetItemFromSlot(leftCheck.x, leftCheck.y).GetItemType())
        {
            leftCheck += Vector2Int.left;
            if (leftCheck.x >= 0 && !slots.IsEmptySlot(leftCheck.x,leftCheck.y) && currentItem == slots.GetItemFromSlot(leftCheck.x, leftCheck.y).GetItemType())
            {
                SlotCheck[leftCheck.x, leftCheck.y] = true;
                horizontalCnt++;
            }
          
        }

        while (upCheck.y < context.gridSize.y && !slots.IsEmptySlot(upCheck.x,upCheck.y) && currentItem == slots.GetItemFromSlot(upCheck.x, upCheck.y).GetItemType() )
        {
            upCheck += Vector2Int.up;
            if (upCheck.y < context.gridSize.y&& !slots.IsEmptySlot(upCheck.x,upCheck.y) &&
                currentItem == slots.GetItemFromSlot(upCheck.x, upCheck.y).GetItemType())
            {
                SlotCheck[upCheck.x, upCheck.y] = true;
                vertiaclCnt++;
            }
        }        
        
        while (downCheck.y >= 0 && !slots.IsEmptySlot(downCheck.x,downCheck.y) && currentItem == slots.GetItemFromSlot(downCheck.x, downCheck.y).GetItemType() )
        {
            downCheck += Vector2Int.down;
            if (downCheck.y >= 0 && !slots.IsEmptySlot(downCheck.x,downCheck.y) && currentItem == slots.GetItemFromSlot(downCheck.x, downCheck.y).GetItemType())
            {
                SlotCheck[downCheck.x, downCheck.y] = true;
                vertiaclCnt++;
            }
        }
        
        if (vertiaclCnt >= 3)
        {
            for (int i = 0; i < context.gridSize.y;i++)
            {
                if (SlotCheck[x, i])
                {
                    slots.RemoveASlot(x,i);
                }
            }
        }

        if (horizontalCnt >= 3)
        {
            for (int i = 0; i < context.gridSize.y;i++)
            {
                if (SlotCheck[i, y])
                {
                    slots.RemoveASlot(i,y);
                }
            }
        }
        
    }
    
    public void OnDrawGizmos()
    {
        if (gridDraw == null) return;
        gridDraw.ChangeGridContext(context);
        gridDraw.Draw();
    }

    // Ulits
    public void CreateBoard()
    {
        FillItemToBoard();
    }

    public void ClearBoard()
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
}