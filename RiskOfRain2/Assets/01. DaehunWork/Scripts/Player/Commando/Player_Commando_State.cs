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
		if (_player.InputMove == Vector2.zero)
		{
			_player.SetFloat("PosX", 0f);
			_player.SetFloat("PosY", 1f);
		}
		else
		{
			_player.SetFloat("PosX", _player.InputMove.x);
			_player.SetFloat("PosY", _player.InputMove.y);
		}
		_player.SetTrigger(Global.PLAYER_UTILITY_SKILL);
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
		_player.SetState(new Player_IdleState(_player));
	}

	public void AnimationChange()
	{

	}

}