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
		/// <summary>
		/// 스킬 이름
		/// </summary>
		/// <value></value>
		public string SkillName { get { return _skillName; } protected set { _skillName = value; } }
		/// <summary>
		/// 스킬 정보
		/// </summary>
		/// <value></value>
		public string SkillInfo { get { return _skillInfo; } protected set { _skillInfo = value; } }
		/// <summary>
		/// 스킬 최대 스택
		/// </summary>
		/// <value></value>
		public int SkillMaxStack { get { return _skillMaxStack; } protected set { _skillMaxStack = value; } }
		/// <summary>
		/// 스킬 현재 스택
		/// </summary>
		/// <value></value>
		public int SkillStack { get { return _skillStack; } protected set { _skillStack = value; } }
		/// <summary>
		/// 스킬 쿨타임
		/// </summary>
		/// <value></value>
		public float SkillCooltime { get { return _skillCoolTime; } protected set { _skillCoolTime = value; } }
		/// <summary>
		/// 스킬 쿨타임이 진행중인지 확인
		/// </summary>
		/// <value></value>
		public bool IsSkillCoolTime { get { return _isSkillCoolTime; } protected set { _isSkillCoolTime = value; } }
		/// <summary>
		/// 스킬 배율
		/// </summary>
		/// <value></value>
		public float Multiplier { get { return _multiplier; } protected set { _multiplier = value; } }
		/// <summary>
		/// 발동 계수(아이템에 타격 시 효과가 붙어있는 경우 해당 발동 계수 값이 추가적으로 연산이 진행 됨)
		/// </summary>
		/// <value></value>
		public float ActivationFactor { get { return _activationFactor; } protected set { _activationFactor = value; } }
		#endregion

		/// <summary>
		/// 스킬 초기화 함수
		/// </summary>
		/// <param name="player"></param>
		public virtual void Init(PlayerBase player)
		{
			_player = player;
		}

		/// <summary>
		/// 스킬 사용 함수
		/// </summary>
		/// <param name="isPressed"></param>
		public virtual void Action(bool isPressed) { }

		/// <summary>
		/// 플레이어 스킬 쿨타임 동작 함수
		/// </summary>
		/// <param name="value">true : 스킬 사용, false : 스택 추가로 인한 추가 쿨타임 가동</param>
		/// <returns></returns>
		public IEnumerator SkillCoolTimeRunning(bool value)
		{
			if (SkillMaxStack < SkillStack)
			{
				SkillStack = SkillMaxStack;
			}

			if (value)
			{
				if (SkillStack - 1 <= 0)
				{
					SkillStack = 0;
				}
				else
				{
					SkillStack -= 1;
				}
			}

			if (IsSkillCoolTime)
			{
				yield break;
			}

			IsSkillCoolTime = true;
			Debug.Log($"Skill Cool Time Start");
			yield return new WaitForSeconds(SkillCooltime);
			Debug.Log($"Skill Cool Time Stop");
			IsSkillCoolTime = false;
			if (SkillMaxStack <= SkillStack + 1)
			{
				SkillStack = SkillMaxStack;
			}
			else
			{
				SkillStack += 1;
			}

			if (SkillStack < SkillMaxStack)
			{
				_player.StartCoroutine(SkillCoolTimeRunning(false));
			}
		}

		public virtual void MultiplierChanged(float value)
		{
			Multiplier += value;
		}

		public virtual void AddSkillMaxStack(int value)
		{
			SkillMaxStack += value;
			_player.StartCoroutine(SkillCoolTimeRunning(false));
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

		/// <summary>
		/// 스킬 사용 가능 체크 함수
		/// </summary>
		/// <returns>true: 사용 가능 false: 사용 불가능</returns>
		public virtual bool SkillAvailableCheck()
		{
			if (SkillStack <= 0)
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