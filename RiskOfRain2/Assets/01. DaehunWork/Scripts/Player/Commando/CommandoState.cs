using UnityEngine;
public class Commando_RollState : IState
{
    private Player _player;
    public Commando_RollState(Player player_)
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