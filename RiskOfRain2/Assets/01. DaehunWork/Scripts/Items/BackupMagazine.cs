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
			// 여기다가 플레이어 UI의 스킬 스택을 갱신하는 로직 추가해주기!!
			UIManager.Instance.PlayerSkillStackSync(1);
		}

		public override void ItemRemove()
		{

		}
	}
}