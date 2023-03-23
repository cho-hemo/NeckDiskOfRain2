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
        _player.SetFloat("PosX", 0);
        _player.SetFloat("PosY", 0);
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

public class Player_WalkState : IState
{
    private Player _player;
    public Player_WalkState(Player player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
        // _player.SetFloat("PosX", _player.InputMove.x);
        // _player.SetFloat("PosY", _player.InputMove.y);
        _player.SetFloat("PosY", 1f);
        _player.SetBool("IsMove", _player.IsMove);
    }
    public void UpdateState()
    {
        if (_player.InputMove == Vector2.zero)
        {
            _player.SetState(new Player_IdleState(_player));
            return;
        }
        else if (_player.IsSprint)
        {
            _player.SetState(new Player_SprintState(_player));
        }
        // _player.SetFloat("PosX", _player.InputMove.x);
        // _player.SetFloat("PosY", _player.InputMove.y);
        _player.Move();
    }

    public void OnExit()
    {
        _player.SetBool("IsMove", _player.IsMove);
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
        _player.SetBool("IsSprint", _player.IsSprint);
    }
    public void UpdateState()
    {
        if (_player.InputMove == Vector2.zero)
        {
            _player.SetState(new Player_IdleState(_player));
            return;
        }
        else if (!_player.IsSprint)
        {
            _player.SetState(new Player_WalkState(_player));
        }
        _player.Move();
    }

    public void OnExit()
    {
        _player.SetBool("IsSprint", _player.IsSprint);
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
        _player.SetTrigger("Jump");
    }
    public void UpdateState()
    {
        _player.Move();
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

public class Player_FlightState : IState
{
    private Player _player;
    public Player_FlightState(Player player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
    }
    public void UpdateState()
    {
        _player.Move();
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
        _player.SetBool("IsDead", _player.IsDead);
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