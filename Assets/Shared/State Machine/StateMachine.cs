public class StateMachine
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Enter(); 
        currentState = newState;
        currentState?.Exit(); 
    }

    public void Update()
    {
        currentState?.Update(); 
    }
}