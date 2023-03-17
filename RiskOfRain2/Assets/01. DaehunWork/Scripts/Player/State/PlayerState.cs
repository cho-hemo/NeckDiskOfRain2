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
        _player.SetBool("IsMove", true);
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
        _player.SetBool("IsSprint", _player.IsSprint);
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
        _player.SetTrigger("Jump");
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