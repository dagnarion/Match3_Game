using UnityEngine;

public class RefillState : IState
{
    private StateMachine stateMachine;
    private BoardController boardController;
    public RefillState(BoardController boardController,StateMachine stateMachine)
    {
        this.boardController = boardController;
        this.stateMachine = stateMachine;
    }
    
    public void Enter()
    {
        boardController.BoardModel.FillItemToBoard();
        boardController.GravityModel.ApplyGravity();
    }

    public void Update()
    {
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
