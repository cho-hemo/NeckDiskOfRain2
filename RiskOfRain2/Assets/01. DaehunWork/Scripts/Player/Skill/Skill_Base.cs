using UnityEngine;
using System;
namespace RiskOfRain2.Player
{
	[Serializable]
	public class Skill
	{
		[SerializeField]
		[Tooltip("스킬 이름")]
		protected string _skillName = default;
		[SerializeField]
		[Tooltip("스킬 정보")]
		protected string _skillInfo = default;
		[SerializeField]
		[Tooltip("스킬 스택")]
		protected int _skillStack = default;
		[SerializeField]
		[Tooltip("스킬 쿨타임")]
		protected float _skillCoolTime = default;
		[SerializeField]
		[Tooltip("스킬 배율")]
		protected float _multiplier = default;
		[SerializeField]
		[Tooltip("스킬 발동계수")]
		protected float _activationFactor = default;
		protected PlayerBase _player = default;

		public string SkillName { get { return _skillName; } protected set { _skillName = value; } }
		public string SkillInfo { get { return _skillInfo; } protected set { _skillInfo = value; } }
		public int SkillStack { get { return _skillStack; } protected set { _skillStack = value; } }
		public float SkillCooltime { get { return _skillCoolTime; } protected set { _skillCoolTime = value; } }
		public float Multiplier { get { return _multiplier; } protected set { _multiplier = value; } }
		public float ActivationFactor { get { return _activationFactor; } protected set { _activationFactor = value; } }

		public virtual void Init(PlayerBase player)
		{
			_player = player;
		}
		public virtual void Action(bool isPressed) { }

		public virtual void MultiplierChanged(float value)
		{
			Multiplier += value;
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

	}
}