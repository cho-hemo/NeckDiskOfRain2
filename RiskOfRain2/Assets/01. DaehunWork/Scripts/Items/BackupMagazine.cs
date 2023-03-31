using RiskOfRain2.Manager;
using RiskOfRain2.Player;

namespace RiskOfRain2.Item
{
	public class BackupMagazine : ItemBase
	{
		public override void ItemInit()
		{
			itemName = "보조 탄창";
			itemInfo = "보조 스킬에 +1 충전을 추가합니다.";
			itemType = ItemType.STATUS;
			itemNumber = 0;
			value = 1f;
		}

		public override void ItemAction()
		{

		}

		public override void ItemGet()
		{
			GameManager.Instance.Player.Skills[1].AddSkillMaxStack((int)value);
		}

		public override void ItemRemove()
		{

		}
	}
}