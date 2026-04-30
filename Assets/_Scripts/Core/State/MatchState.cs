using UnityEngine;

public class MatchState : IState
{
    private StateMachine stateMachine;
    private BoardController boardController;
    public MatchState(BoardController boardController,StateMachine stateMachine)
    {
        this.boardController = boardController;
        this.stateMachine = stateMachine;
    }
    
    public void Enter()
    {
        boardController.MatchModel.Match();
    }

    public void Update()
    {
        Debug.Log("On Match State");
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
        
        stateMachine.ChangeState(boardController.GravityState);
        
    }

    public void Exit()
    {
        
    }
}
