using UnityEngine;

public class GravityState : IState
{
    private StateMachine stateMachine;
    private BoardController boardController;
    public GravityState(BoardController boardController,StateMachine stateMachine)
    {
        this.boardController = boardController;
        this.stateMachine = stateMachine;
    }
    
    public void Enter()
    {
        boardController.GravityModel.ApplyGravity();
        stateMachine.ChangeState(boardController.RefillState);
    }

    public void Update()
    {
        // if (boardController.boardView.CompleteTransition)
        // {
        //     stateMachine.ChangeState(boardController.RefillState);
        //     return;
        // }
         Debug.Log("On Gravity State");
    }

    public void Exit()
    {
        
    }
}
