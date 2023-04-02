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
			Multiplier = 1.0f;
			ActivationFactor = 1.0f;
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
			if (Physics.Raycast(ray_, out RaycastHit rayCastHit_, _player.RayRange, _player.layerMask))
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
			SkillCooltime = 3.0f;
			Multiplier = 3.0f;
			ActivationFactor = 1.0f;
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
			if (Physics.Raycast(ray_, out RaycastHit rayCastHit_, _player.RayRange, _player.layerMask))
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
			GameObject bullet_ = ObjectPoolManager.Instance.ObjectPoolPop("Phase_Round_Bullet");
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
		public override void Init(PlayerBase player)
		{
			base.Init(player);
			SkillName = "PhaseBlast";
			SkillInfo = "총 8x200% 피해를 입히는 두 개의 근거리 폭발물을 발사합니다.";
			SkillMaxStack = 1;
			SkillStack = SkillMaxStack;
			SkillCooltime = 3.0f;
			Multiplier = 2.0f;
			ActivationFactor = 0.5f;
		}
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
		public override void Init(PlayerBase player)
		{
			base.Init(player);
			SkillName = "TacticalSlide";
			SkillInfo = "짧은 거리를 미끄러져 나아갑니다. 슬라이드 동안 사격할 수 있습니다.";
			SkillMaxStack = 1;
			SkillStack = SkillMaxStack;
			SkillCooltime = 4.0f;
			Multiplier = 0f;
			ActivationFactor = 0f;
		}

		public override void Action(bool isPressed)
		{
			if (isPressed && SkillAvailableCheck())
			{
				_player.SetState(new TacticalSlideState(_player as Player_Commando));
			}
		}
	}

	public class SuppressiveFire : Skill
	{
		public override void Init(PlayerBase player)
		{
			base.Init(player);
			SkillName = "SuppressiveFire";
			SkillInfo = "마비. 연속 사격하여 탄환당 100% 피해를 줍니다. 공격 속도에 따라 발사 횟수가 증가합니다.";
			SkillMaxStack = 1;
			SkillStack = SkillMaxStack;
			SkillCooltime = 9.0f;
			Multiplier = 1.0f;
			ActivationFactor = 1.0f;
		}

		public override void Action(bool isPressed)
		{
			if (isPressed && SkillAvailableCheck())
			{
				_player.StartCoroutine(SuppressiveFireShoot());
			}
		}

		public IEnumerator SuppressiveFireShoot()
		{
			int count_ = Mathf.RoundToInt(_player.AttackSpeed * 6);
			float delay = (float)1 / count_;
			for (int i = 0; i < count_; i++)
			{
				// Debug.Log($"Special Skill Shoot / Delay : {delay}");
				SkillShot();
				_player.PlayerAnimator.Play("SpecialSkill", PlayerDefine.PLAYER_ATTACK_LAYER);
				_player.SetFloat("AttackSpeed", count_);
				yield return new WaitForSeconds(delay);
			}
			_player.SetFloat("AttackSpeed", _player.AttackSpeed);
			_player.PlayerAnimator.StopPlayback();
		}

		public void SkillShot()
		{
			Vector3 pos_ = default;
			Quaternion rotation_ = default;

			pos_ = _player.FocusPoint[1].position;
			rotation_ = RayShoot(pos_);
			BulletShoot(pos_, rotation_);
		}

		public Quaternion RayShoot(Vector3 start)
		{
			Vector2 screenCenterPoint_ = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
			Ray ray_ = _player.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
			if (Physics.Raycast(ray_, out RaycastHit rayCastHit_, _player.RayRange, _player.layerMask))
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

	public class FragGrenade : Skill
	{

	}
}
