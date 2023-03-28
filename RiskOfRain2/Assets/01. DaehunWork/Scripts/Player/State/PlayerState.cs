using UnityEngine;

namespace RiskOfRain2.Player
{
	public class Player_IdleState : IState
	{
		private PlayerBase _player;
		public Player_IdleState(PlayerBase player_)
		{
			this._player = player_;
		}

		public void OnEnter()
		{
		}

		public void UpdateState()
		{
			if (_player.InputMove != Vector2.zero)
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
		}

		public void AnimationChange()
		{

		}
	}

	public class Player_WalkState : IState
	{
		private PlayerBase _player;
		public Player_WalkState(PlayerBase player_)
		{
			this._player = player_;
		}

		public void OnEnter()
		{
			// _player.SetFloat("PosX", _player.InputMove.x);
			// _player.SetFloat("PosY", _player.InputMove.y);
			_player.SetFloat("PosY", 1f);
			_player.SetBool("IsMove", _player.IsMove);
			Debug.Log("move");
			float speed_ = _player.CurrentWalkSpeed / _player.DefaultWalkSpeed;
			_player.SetFloat(PlayerDefine.MOVE_SPEED, speed_);
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
		private PlayerBase _player;
		public Player_SprintState(PlayerBase player_)
		{
			this._player = player_;
		}

		public void OnEnter()
		{
			_player.SetBool("IsSprint", _player.IsSprint);

			float speed_ = _player.CurrentSprintSpeed / _player.DefaultSprintSpeed;
			_player.SetFloat(PlayerDefine.MOVE_SPEED, speed_);
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
		private PlayerBase _player;
		public Player_JumpState(PlayerBase player_)
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
			if (!_player.IsGrounded)
			{
				_player.SetState(new Player_FlightState(_player));
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

	public class Player_FlightState : IState
	{
		private PlayerBase _player;
		public Player_FlightState(PlayerBase player_)
		{
			this._player = player_;
		}

		public void OnEnter()
		{
			_player.SetBool("IsGrounded", _player.IsGrounded);
		}

		public void UpdateState()
		{
			_player.Move();
			if (_player.IsGrounded)
			{
				_player.SetState(new Player_IdleState(_player));
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

	public class Player_DeadState : IState
	{
		private PlayerBase _player;
		public Player_DeadState(PlayerBase player_)
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
}