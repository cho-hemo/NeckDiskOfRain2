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
			_player.SetSkillAvailable(false);
			// if (_player.InputMove == Vector2.zero)
			// {
			// 	_player.SetFloat("PosX", 0f);
			// 	_player.SetFloat("PosY", 1f);
			// }
			// else
			// {
			// 	_player.SetFloat("PosX", _player.InputMove.x);
			// 	_player.SetFloat("PosY", _player.InputMove.y);
			// }

			_player.SetFloat("PosX", 0f);
			_player.SetFloat("PosY", 1f);
			_player.SetTrigger(PlayerDefine.PLAYER_UTILITY_SKILL);
		}

		public void UpdateState()
		{
			// Debug.Log(_player.GetCurrentAnimatorStateInfo(0).normalizedTime);
			_player.Roll();
			if (1f <= _player.GetCurrentAnimatorStateInfo(0).normalizedTime && !_player.GetCurrentAnimatorStateInfo(0).loop)
			{
				Debug.Log($"Animation End");
				ChangeState();
			}
		}

		public void OnExit()
		{
			_player.SetSkillAvailable(true);
		}

		public void Action()
		{

		}

		public void ChangeState()
		{
			_player.SetTrigger("Exit");
			if (_player.IsGrounded && (_player.InputMove != Vector2.zero))
			{
				if (_player.IsSprint || _player.InputMove.y <= 0 || _player.IsShot)
				{
					_player.SetState(new Player_WalkState(_player));
				}
				else
				{
					_player.SetState(new Player_SprintState(_player));
				}
			}
			else
			{
				_player.SetState(new Player_IdleState(_player));
			}
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