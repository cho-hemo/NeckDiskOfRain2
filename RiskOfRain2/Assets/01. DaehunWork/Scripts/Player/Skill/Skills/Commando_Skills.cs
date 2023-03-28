using System.Collections;
using UnityEngine;
using RiskOfRain2.Manager;
using RiskOfRain2.Bullet;
using UnityEngine.InputSystem;

namespace RiskOfRain2.Player.Commando
{
	public class DoubleTap : Skill
	{
		public override void Init(PlayerBase player)
		{
			base.Init(player);
			SkillName = "DoubleTap";
			SkillInfo = "적 하나를 빠르게 쏘아 100%의 피해를 입힙니다.";
			SkillMaxStack = 1;
			SkillStack = SkillMaxStack;
			SkillCooltime = 0f;
			Multiplier = 1f;
			ActivationFactor = 1f;
		}

		public override void Action(bool isPressed)
		{
			if (isPressed && SkillAvailableCheck())
			{
				MainSkillShot();
			}
		}

		public void MainSkillShot()
		{
			Vector3 pos_ = default;
			Quaternion rotation_ = default;
			AnimatorStateInfo currentStateInfo_ = _player.PlayerAnimator.GetCurrentAnimatorStateInfo(PlayerDefine.PLAYER_ATTACK_LAYER);
			if (currentStateInfo_.IsName("FirePistol_Left"))
			{
				pos_ = _player.FocusPoint[0].position;
				rotation_ = RayShoot(pos_);
				BulletShoot(pos_, rotation_);
			}
			else if (currentStateInfo_.IsName("FirePistol_Right"))
			{
				pos_ = _player.FocusPoint[1].position;
				rotation_ = RayShoot(pos_);
				BulletShoot(pos_, rotation_);
			}
		}

		public Quaternion RayShoot(Vector3 start)
		{
			Vector2 screenCenterPoint_ = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
			Ray ray_ = _player.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
			if (Physics.Raycast(ray_, out RaycastHit rayCastHit_, _player.RayRange))
			{
				Vector3 direction = rayCastHit_.point - start;
				return Quaternion.LookRotation(direction);
			}
			else
			{
				return _player.transform.rotation;
			}
		}

		public void BulletShoot(Vector3 pos, Quaternion rotation)
		{
			GameObject bullet_ = ObjectPoolManager.Instance.ObjectPoolPop("NormalBullet");
			if (bullet_.Equals(null) || bullet_.Equals(default))
			{
				return;
			}
			bullet_.transform.localPosition = pos;
			bullet_.transform.rotation = rotation;
			bullet_.SetActive(true);
		}
	}

	public class PhaseRound : Skill
	{
		public override void Init(PlayerBase player)
		{
			base.Init(player);
			SkillName = "PhaseRound";
			SkillInfo = "관통력이 있는 총알을 발사하여 300% 피해를 입힙니다. 적을 관통할 때마다 피해가 40% 증가합니다.";
			SkillMaxStack = 1;
			SkillStack = SkillMaxStack;
			SkillCooltime = 3f;
			Multiplier = 3f;
			ActivationFactor = 1f;
		}

		public override void Action(bool isPressed)
		{
			if (isPressed && SkillAvailableCheck())
			{
				SubSkillShot();
			}
		}

		public void SubSkillShot()
		{
			Vector3 pos_ = default;
			Quaternion rotation_ = default;
			AnimatorStateInfo currentStateInfo_ = _player.PlayerAnimator.GetCurrentAnimatorStateInfo(PlayerDefine.PLAYER_ATTACK_LAYER);
			pos_ = _player.FocusPoint[2].position;
			rotation_ = RayShoot(pos_);
			BulletShoot(pos_, rotation_);
		}

		public Quaternion RayShoot(Vector3 start)
		{
			Vector2 screenCenterPoint_ = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
			Ray ray_ = _player.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
			if (Physics.Raycast(ray_, out RaycastHit rayCastHit_, _player.RayRange))
			{
				Vector3 direction = rayCastHit_.point - start;
				return Quaternion.LookRotation(direction);
			}
			else
			{
				return _player.transform.rotation;
			}
		}

		public void BulletShoot(Vector3 pos, Quaternion rotation)
		{
			GameObject bullet_ = ObjectPoolManager.Instance.ObjectPoolPop("SubSkillBullet");
			if (bullet_.Equals(null) || bullet_.Equals(default))
			{
				return;
			}
			bullet_.transform.localPosition = pos;
			bullet_.transform.rotation = rotation;
			bullet_.SetActive(true);
		}
	}

	public class PhaseBlast : Skill
	{

	}

	public class TacticalDive : Skill
	{
		public override void Init(PlayerBase player)
		{
			base.Init(player);
			SkillName = "TacticalDive";
			SkillInfo = "짧은 거리를 몸을 굴려 이동합니다.";
			SkillMaxStack = 1;
			SkillStack = SkillMaxStack;
			SkillCooltime = 4f;
			Multiplier = 0f;
			ActivationFactor = 0f;
		}

		public override void Action(bool isPressed)
		{
			if (isPressed && SkillAvailableCheck())
			{
				_player.SetState(new TacticalDiveState(_player as Player_Commando));
			}
		}
	}

	public class TacticalSlide : Skill
	{

	}

	public class SuppressiveFire : Skill
	{

	}

	public class FragGrenade : Skill
	{

	}
}
