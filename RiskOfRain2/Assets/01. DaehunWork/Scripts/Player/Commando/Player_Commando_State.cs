using UnityEngine;
using RiskOfRain2;

namespace RiskOfRain2.Player.Commando
{
	public class TacticalDiveState : IState
	{
		private Player_Commando _player;
		public TacticalDiveState(Player_Commando player_)
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
			_player.SetTrigger(PlayerDefine.PLAYER_UTILITY_SKILL);
		}

		public void UpdateState()
		{
			_player.Roll();
			if (!_player.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
			{
				ChangeState();
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
			_player.SetState(new Player_IdleState(_player));
		}

		public void AnimationChange()
		{

		}
	}

	public class TacticalSlideState : IState
	{
		private Player_Commando _player;
		public TacticalSlideState(Player_Commando player_)
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
			_player.SetTrigger(PlayerDefine.PLAYER_UTILITY_SKILL);
		}

		public void UpdateState()
		{
			_player.Roll();
			if (!_player.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
			{
				ChangeState();
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
			_player.SetState(new Player_IdleState(_player));
		}

		public void AnimationChange()
		{

		}
	}
}