using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RiskOfRain2.Item
{
	public abstract class ItemBase : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("아이템 이름")]
		protected string _itemName;
		[SerializeField]
		[Tooltip("아이템 정보")]
		protected string _itemInfo;
		[SerializeField]
		[Tooltip("아이템 타입")]
		protected ItemType _itemType;

		/// <summary>
		/// 아이템의 동작을 수행 할 함수
		/// </summary>
		public abstract void ItemAction();
	}


	/// <summary>
	/// 아이템의 발동 조건
	/// STATUS : 스탯 증감소 Ex) 공속 증가, 이속 증가, 스킬 스택 증가...
	/// DAMAGE : 타격 시 발동 Ex) 타격 시 N% 확률로 출혈
	/// HIT : 피격 시 발동 Ex) 피해를 받고 2초 후 체력 회복
	/// INTERACTION : 상호작용 시 발동 Ex) 상호작용 시 폭죽이 발사, 텔레포터 이벤트 상호작용 시 깃발 생성
	/// </summary>
	public enum ItemType
	{
		NONE, STATUS, DAMAGE, HIT, INTERACTION
	}
}

