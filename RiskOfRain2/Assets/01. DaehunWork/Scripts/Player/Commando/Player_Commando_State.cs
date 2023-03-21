using UnityEngine;
public class Player_Commando_RollState : IState
{
    private Player_Commando _player;
    public Player_Commando_RollState(Player_Commando player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
        _player.SetFloat("PosX", _player.InputMove.x);
        _player.SetFloat("PosY", _player.InputMove.y);
        _player.PlayerAnimator.SetTrigger(Global.PLAYER_UTILITY_SKILL);
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