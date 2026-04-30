using UnityEngine;

public class SwapState : IState
{
    private StateMachine stateMachine;
    private BoardController boardController;

    public SwapState(BoardController boardController, StateMachine stateMachine)
    {
        this.boardController = boardController;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        boardController.SwapingController.SetUpHadSwap(false);
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