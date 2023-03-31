using RiskOfRain2.Manager;
using RiskOfRain2.Player;

namespace RiskOfRain2.Item
{
	public class SoldiersSyringe : ItemBase
	{
		public override void ItemInit()
		{
			itemName = "군인의 주사기";
			itemInfo = "공격 속도가 15%(중첩당 +15%) 증가합니다.";
			itemType = ItemType.STATUS;
			itemNumber = 0;
			value = 0.15f;
			targetStat = PlayerStat.ATTACK_SPEED;
		}

		public override void ItemAction()
		{

		}

		public override void ItemGet()
		{
			GameManager.Instance.Player.IncreaseStat(targetStat, value * itemNumber);
		}

		public override void ItemRemove()
		{

		}
	}
}