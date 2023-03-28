using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using RiskOfRain2.Manager;
using RiskOfRain2.Bullet;

namespace RiskOfRain2.Player.Commando
{
	public class Player_Commando : PlayerBase
	{
		private Vector2 _rollDirection = default;
		protected new void Start()
		{
			base.Start();
			IsSkillAvailable = true;
			PlayerType = PlayerType.COMMANDO;
			Skills.Add(new DoubleTap());
			Skills.Add(new PhaseRound());
			Skills.Add(new TacticalDive());
			Skills.Add(new SuppressiveFire());
			foreach (var skill in Skills)
			{
				skill.Init(this);
			}
		}
		public override void PassiveSkill()
		{
		}

		public override void MainSkill(bool isPressed)
		{
			bool isSkillAvailable_ = SkillAvailableCheck(PlayerDefine.PLAYER_MAIN_SKILL_INDEX);

			if (!isSkillAvailable_)
			{
				return;
			}

			if (isPressed)
			{
				IsSprint = false;
			}

			IsShot = isPressed;
			SetBool(PlayerDefine.PLAYER_IS_MAIN_SKILL, isPressed);
			SetFloat(PlayerDefine.ATTACK_SPEED, AttackSpeed);
		}

		public void MainSkillAction()
		{
			Skill skill_ = Skills[PlayerDefine.PLAYER_MAIN_SKILL_INDEX];
			skill_.Action(true);
		}

		public override void SubSkill(bool isPressed)
		{
			bool isSkillAvailable_ = SkillAvailableCheck(PlayerDefine.PLAYER_SUB_SKILL_INDEX);
			if (!isSkillAvailable_)
			{
				return;
			}

			if (isPressed)
			{
				SkillAction(PlayerDefine.PLAYER_SUB_SKILL_INDEX, isPressed);
				SetTrigger(PlayerDefine.PLAYER_SUB_SKILL);
			}
		}




		public void SubSKillAction(bool isPressed)
		{
			Skill skill_ = Skills[PlayerDefine.PLAYER_SUB_SKILL_INDEX];
			skill_.Action(isPressed);
			StartCoroutine(skill_.SkillCoolTimeRunning());
		}

		public override void UtilitySkill(bool isPressed)
		{
			bool isSkillAvailable_ = SkillAvailableCheck(PlayerDefine.PLAYER_UTILITY_SKILL_INDEX);

			if (!isSkillAvailable_)
			{
				return;
			}

			if (isPressed)
			{
				_rollDirection = InputMove;
				SkillAction(PlayerDefine.PLAYER_UTILITY_SKILL_INDEX, isPressed);
			}
		}

		public override void SpecialSkill(bool isPressed)
		{
			bool isSkillAvailable_ = SkillAvailableCheck(PlayerDefine.PLAYER_SPECIAL_SKILL_INDEX);

			if (!isSkillAvailable_)
			{
				return;
			}

			if (isPressed)
			{
				StartCoroutine(SpecialSkillCoroutine(isPressed));
				IsSprint = false;
			}
		}

		public void SkillAction(int index, bool isPressed)
		{
			Skill skill_ = Skills[index];
			skill_.Action(isPressed);
			StartCoroutine(skill_.SkillCoolTimeRunning());
		}

		IEnumerator SpecialSkillCoroutine(bool isPressed)
		{
			int loopCount_ = Mathf.RoundToInt(AttackSpeed * 6);
			for (int i = 0; i < loopCount_; i++)
			{
				SetTrigger(PlayerDefine.PLAYER_SPECIAL_SKILL);
				yield return new WaitForSeconds((float)1 / loopCount_);
				AnimatorStateInfo currentState_ = GetCurrentAnimatorStateInfo(PlayerDefine.PLAYER_ATTACK_LAYER);
				PlayerAnimator.Play("", PlayerDefine.PLAYER_ATTACK_LAYER, currentState_.normalizedTime);
				Debug.Log($"SHOT");
				if (currentState_.IsName("SpecialSKill"))
				{
				}
			}
		}

		public void Roll()
		{
			float targetSpeed = IsSprint ? CurrentSprintSpeed : CurrentWalkSpeed;

			if (_rollDirection == Vector2.zero) targetSpeed = 0.0f;

			float currentHorizontalSpeed = new Vector3(CharacterController.velocity.x, 0.0f, CharacterController.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = KeyInputManager.Instance.analogMovement ? _rollDirection.magnitude : 1f;

			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				CurrentSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				CurrentSpeed = Mathf.Round(CurrentSpeed * 1000f) / 1000f;
			}
			else
			{
				CurrentSpeed = targetSpeed;
			}

			Vector3 rollDirection_ = new Vector3(_rollDirection.x, 0, _rollDirection.y);
			Vector3 targetDirection_ = (Quaternion.Euler(0, MainCamera.transform.eulerAngles.y, 0) * rollDirection_).normalized;
			CharacterController.Move(targetDirection_ * (CurrentSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		}
	}

}