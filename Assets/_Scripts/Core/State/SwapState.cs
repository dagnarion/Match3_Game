using UnityEngine;

public class SwapState : IState
{
    private StateMachine stateMachine;
    private BoardController boardController;
    private Vector2Int possibleMove;
    public SwapState(BoardController boardController, StateMachine stateMachine)
    {
        this.boardController = boardController;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        boardController.SwapingController.SetUpHadSwap(false);
        possibleMove = boardController.HintModel.GetPosibleMove();
        if (possibleMove == new Vector2Int(-1, -1))
        {
            Debug.Log("Reshuffle");
            for (int x = 0; x < boardController.Config.GridSize.x; x++)
            for (int y = 0; y < boardController.Config.GridSize.y; y++)
            {
                if (boardController.GridModel.IsEmptyCell(x, y)) continue;
                    boardController.boardView.RemoveItemOnCell(boardController.GridModel.GetCell(x, y).ID);
            }
            boardController.ReshuffleModel.Reshuffle();
            boardController.GravityModel.ApplyGravity();
        }
    }

    public void Update()
    {
        
        if (!boardController.SwapingController.HadSwap)
        {
            boardController.SwapingController.SwapingHandle();
            return;
        }

        if (boardController.boardView.CompleteTransition)
        {
            stateMachine.ChangeState(boardController.MatchState);
            return;
        }
    }

    public void Exit()
    {
       
    }
}