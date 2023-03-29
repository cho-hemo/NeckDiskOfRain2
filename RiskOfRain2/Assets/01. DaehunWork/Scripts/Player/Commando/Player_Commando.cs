using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using RiskOfRain2.Manager;

namespace RiskOfRain2.Player.Commando
{
	public class Player_Commando : PlayerBase
	{
		private Vector2 _rollDirection = default;
		private float _mainCameraYRotation = default;

		protected void Awake()
		{
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

		protected new void Start()
		{
			base.Start();
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
			SkillAction(PlayerDefine.PLAYER_MAIN_SKILL_INDEX, true);
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

		public override void UtilitySkill(bool isPressed)
		{
			bool isSkillAvailable_ = SkillAvailableCheck(PlayerDefine.PLAYER_UTILITY_SKILL_INDEX);

			if (!isSkillAvailable_)
			{
				return;
			}

			if (isPressed)
			{
				if (InputMove == Vector2.zero)
				{
					_rollDirection = new Vector2(0, 1f);
				}
				else
				{
					_rollDirection = InputMove;
				}
				_mainCameraYRotation = MainCamera.transform.eulerAngles.y;
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
				IsSprint = false;
				SkillAction(PlayerDefine.PLAYER_SPECIAL_SKILL_INDEX, isPressed);
			}
		}

		public void Roll()
		{
			Vector3 rollDirection_ = new Vector3(_rollDirection.x, 0, _rollDirection.y);
			Vector3 targetDirection_ = (Quaternion.Euler(0, _mainCameraYRotation, 0) * rollDirection_).normalized;
			Vector3 move = targetDirection_ * (CurrentSpeed * 2 * Time.deltaTime);
			CharacterController.Move(move);
		}
	}

}