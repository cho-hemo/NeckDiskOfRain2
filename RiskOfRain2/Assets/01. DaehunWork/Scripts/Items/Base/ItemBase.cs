using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiskOfRain2.Player;

namespace RiskOfRain2.Item
{
	public abstract class ItemBase : MonoBehaviour
	{
		[Tooltip("아이템 이름")]
		public string itemName;

		[Tooltip("아이템 정보")]
		public string itemInfo;

		[Tooltip("아이템 타입")]
		public ItemType itemType;

		[Tooltip("아이템 갯수")]
		public int itemNumber;

		[Tooltip("타겟 스탯")]
		public PlayerStat targetStat;

		[Tooltip("타겟 스킬 인덱스")]
		public int targetSkillIndex;

		[Tooltip("타겟 스킬 스탯")]
		public PlayerSkillStat targetSkillStat;

		[Tooltip("아이템에 수치(발동확률, 스탯 증가 폭)")]
		public float value;

		public abstract void ItemInit();

		/// <summary>
		/// 아이템의 동작을 수행 할 함수
		/// </summary>
		public abstract void ItemAction();
		/// <summary>
		/// 아이템 획득 시 수행 할 함수
		/// </summary>
		public abstract void ItemGet();
		/// <summary>
		/// 아이템 삭제 시 수행 할 함수
		/// </summary>
		public abstract void ItemRemove();

		private void Start()
		{
			ItemInit();
		}
	}

	/// <summary>
	/// 아이템의 발동 조건
	/// STATUS : 스탯 증감소 Ex) 공속 증가, 이속 증가, 스킬 스택 증가...
	/// DAMAGE : 타격 시 발동 Ex) 타격 시 N% 확률로 출혈
	/// HIT : 피격 시 발동 Ex) 피해를 받고 2초 후 체력 회복
	/// INTERACTION : 상호작용 시 발동 Ex) 상호작용 시 폭죽이 발사, 텔레포터 이벤트 상호작용 시 깃발 생성
	/// EQUIP : 사용 시 발동 Ex) 사용 시 체력 50% 회복
	/// </summary>
	public enum ItemType
	{
		NONE, STATUS, DAMAGE, HIT, INTERACTION, EQUIP
	}
}

