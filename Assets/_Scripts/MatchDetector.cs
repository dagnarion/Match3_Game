using System;
using UnityEngine;
using System.Collections.Generic;
public class MatchDetector
{
    private GridContext context;
    private Grid slots;
    private bool[,] SlotCheck;

    public MatchDetector(GridContext context,Grid slot)
    {
        this.context = context;
        this.slots = slot;
        SlotCheck = new bool[context.gridSize.x, context.gridSize.y];
    }

    public void ChangeContext(GridContext context,Grid slot)
    {
        this.context = context;
        this.slots = slot;
        SlotCheck = new bool[context.gridSize.x, context.gridSize.y];
    }
    
    public void MatchAllBoard()
    {
        for(int i = 0;i<context.gridSize.x;i++)
        for (int j = 0; j < context.gridSize.y; j++)
        {
            Match(i, j);
        }
    }
    
    public void Match(int x,int y) // chỗ này có thể abstraction lại để tạo nhiêu kiểu match thay thế nhau
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
}
