using UnityEngine;

public class StateMachine
{
    public IState CurrentState { get; private set; }
    public IState PreviousState { get; private set; }
    public void Initialize(IState state)
    {
        CurrentState = state;
    }

    public void Update()
    {
        CurrentState?.Update();
    }
    
    public void ChangeState(IState state)
    {
        if (state.GetType() == CurrentState.GetType()) return;
        CurrentState?.Exit();
        PreviousState = CurrentState;
        CurrentState = state;
        CurrentState?.Enter();
    }
    
    
    
}
