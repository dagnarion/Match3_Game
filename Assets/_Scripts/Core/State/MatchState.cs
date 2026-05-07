using UnityEngine;
using System.Collections.Generic;
public class MatchState : IState
{
    private StateMachine stateMachine;
    private BoardController boardController;
    private List<(ItemType, Vector2Int)> specialItem;
    public MatchState(BoardController boardController,StateMachine stateMachine)
    {
        this.boardController = boardController;
        this.stateMachine = stateMachine;
    }
    
    public void Enter()
    {
        boardController.MatchModel.Match();
        if (boardController.MatchModel.CanMatch)
        {
            specialItem = boardController.MatchModel.GetSpecialItem(boardController.SwapingController.nextCell);
        }
    }

    public void Update()
    {
        if (!boardController.MatchModel.CanMatch)
        {
            if(stateMachine.PreviousState is SwapState)  boardController.SwapingController.SwapBack();
            stateMachine.ChangeState(boardController.SwapState);
            return;
        }
        for (int x = 0; x < boardController.Config.GridSize.x; x++)
        for (int y = 0; y < boardController.Config.GridSize.y; y++)
        {
            if (boardController.GridModel.IsEmptyCell(x, y)) continue;
            if (boardController.GridModel.GetCell(x, y).IsMatched)
            {
                boardController.boardView.RemoveItemOnCell(boardController.GridModel.GetCell(x, y).ID);
                boardController.GridModel.ClearACell(x, y);
            }
        }

        if (specialItem != null && specialItem.Count != 0)
        {
            foreach (var item in specialItem)
            {
                boardController.BoardModel.CreateSpecialItemToBoard(item.Item1, item.Item2.x, item.Item2.y);
            }
        }

        stateMachine.ChangeState(boardController.GravityState);
        
    }

    public void Exit()
    {
        
    }
}
