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