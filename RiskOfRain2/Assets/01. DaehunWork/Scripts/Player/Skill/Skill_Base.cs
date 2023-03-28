using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace RiskOfRain2.Player
{
	[Serializable]
	public class Skill
	{
		#region Inspector
		[Header("스킬 상세")]

		[SerializeField]
		[Tooltip("스킬 이름")]
		protected string _skillName = default;

		[SerializeField]
		[Tooltip("스킬 정보")]
		protected string _skillInfo = default;

		[SerializeField]
		[Tooltip("스킬 최대 스택")]
		protected int _skillMaxStack = default;

		[SerializeField]
		[Tooltip("스킬 스택")]
		protected int _skillStack = default;

		[SerializeField]
		[Tooltip("스킬 쿨타임")]
		protected float _skillCoolTime = default;

		[SerializeField]
		[Tooltip("스킬 쿨타임 중")]
		protected bool _isSkillCoolTime = default;

		[SerializeField]
		[Tooltip("스킬 배율")]
		protected float _multiplier = default;

		[SerializeField]
		[Tooltip("스킬 발동계수")]
		protected float _activationFactor = default;

		#endregion
		protected PlayerBase _player = default;

		#region Property
		public string SkillName { get { return _skillName; } protected set { _skillName = value; } }
		public string SkillInfo { get { return _skillInfo; } protected set { _skillInfo = value; } }
		public int SkillMaxStack { get { return _skillMaxStack; } protected set { _skillMaxStack = value; } }
		public int SkillStack { get { return _skillStack; } protected set { _skillStack = value; } }
		public float SkillCooltime { get { return _skillCoolTime; } protected set { _skillCoolTime = value; } }
		public bool IsSkillCoolTime { get { return _isSkillCoolTime; } protected set { _isSkillCoolTime = value; } }
		public float Multiplier { get { return _multiplier; } protected set { _multiplier = value; } }
		public float ActivationFactor { get { return _activationFactor; } protected set { _activationFactor = value; } }
		#endregion

		public virtual void Init(PlayerBase player)
		{
			_player = player;
		}

		public virtual void Action(bool isPressed) { }

		public IEnumerator SkillCoolTimeRunning()
		{
			IsSkillCoolTime = true;
			if (SkillStack - 1 <= 0)
			{
				SkillStack = 0;
			}
			else
			{
				SkillStack -= 1;
			}
			//Debug.Log($"Skill Cool Time Running Start");
			yield return new WaitForSeconds(SkillCooltime);
			IsSkillCoolTime = false;
			if (SkillMaxStack <= SkillStack + 1)
			{
				SkillStack = SkillMaxStack;
			}
			else
			{
				SkillStack += 1;
			}
			//Debug.Log($"Skill Cool Time Running Stop");
		}

		public virtual void MultiplierChanged(float value)
		{
			Multiplier += value;
		}

		public virtual void AddSkillMaxStack(int value)
		{
			SkillMaxStack += value;
		}

		public virtual void CoolTimeReduction(float value)
		{
			if (SkillCooltime * value <= 0.5f)
			{
				SkillCooltime = 0.5f;
			}
			else
			{
				SkillCooltime *= value;
			}
		}

		public virtual bool SkillAvailableCheck()
		{
			//Debug.Log($"SkillAvailableCheck IsSkillCoolTime : {IsSkillCoolTime} / SkillStack : {SkillStack} ");
			if (IsSkillCoolTime || SkillStack <= 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

	}
}