public class StateMachine
{
    private IState _currentState = default;

    public void SetState(IState state)
    {
        if (_currentState != null)
        {
            OnExit();
            _currentState = state;
            OnEnter();
        }
        else
        {
            _currentState = state;
            OnEnter();
        }
    }

    public IState GetState()
    {
        return _currentState;
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

    public void AnimationChange()
    {
        _currentState.AnimationChange();
    }



}

public interface IState
{
    public void OnEnter();
    public void UpdateState();
    public void OnExit();
    public void Action();
    public void ChangeState();
    public void AnimationChange();
}

