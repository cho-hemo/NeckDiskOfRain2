public class StateMachine
{
    private State _currentState = default;

    public void SetState(State state)
    {
        if (_currentState != null)
        {
            _currentState.OnExit();
            _currentState = state;
            _currentState.OnEnter();
        }
        else
        {
            _currentState = state;
            _currentState.OnEnter();
        }
    }

    public void UpdateState()
    {
        _currentState.UpdateState();
    }

    public void OnEnter()
    {
        _currentState.OnEnter();
    }
    public void OnExit()
    {
        _currentState.OnExit();
    }

    public void Action()
    {
        _currentState.Action();
    }
    public void ChangeState()
    {
        _currentState.ChangeState();
    }

    public State GetState()
    {
        return _currentState;
    }


}

public interface State
{
    public void OnEnter();
    public void UpdateState();
    public void OnExit();
    public void Action();
    public void ChangeState();
}