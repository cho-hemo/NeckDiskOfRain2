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
        Global.Log("Player_Commando_RollState Debug : OnEnter()");
        _player.PlayerAnimator.SetFloat("PosX", 0);
        _player.PlayerAnimator.SetFloat("PosY", 1);
        //_player.PlayerAnimator.SetBool(Global.PLAYER_IS_UTILITY_SKILL, true);
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