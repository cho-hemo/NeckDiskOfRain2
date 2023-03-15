using UnityEngine;

public class Player_IdleState : IState
{
    private Player _player;
    public Player_IdleState(Player player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
    }

    public void UpdateState()
    {
        if (_player.Input.move != Vector2.zero)
        {
            _player.StateMachine.SetState(new Player_WalkState(_player));
        }
        else if (_player.Input.jump == true)
        {
            _player.StateMachine.SetState(new Player_JumpState(_player));
        }
    }

    public void OnExit()
    {
    }

    public void Action()
    {
    }

    public void ChangeState()
    {
    }

    public void AnimationChange()
    {

    }
}

public class Player_WalkState : IState
{
    private Player _player;
    public Player_WalkState(Player player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
    }
    public void UpdateState()
    {

    }

    public void OnExit()
    {
    }

    public void Action()
    {
    }

    public void ChangeState()
    {
    }
    public void AnimationChange()
    {

    }
}

public class Player_SprintState : IState
{

    private Player _player;
    public Player_SprintState(Player player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
    }
    public void UpdateState()
    {
    }

    public void OnExit()
    {
    }

    public void Action()
    {
    }

    public void ChangeState()
    {
    }
    public void AnimationChange()
    {

    }
}

public class Player_JumpState : IState
{

    private Player _player;
    public Player_JumpState(Player player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
    }
    public void UpdateState()
    {
    }

    public void OnExit()
    {
    }

    public void Action()
    {
    }

    public void ChangeState()
    {
    }
    public void AnimationChange()
    {

    }
}

public class Player_DeadState : IState
{
    private Player _player;
    public Player_DeadState(Player player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
    }
    public void UpdateState()
    {
    }

    public void OnExit()
    {
    }

    public void Action()
    {
    }

    public void ChangeState()
    {
    }
    public void AnimationChange()
    {

    }
}