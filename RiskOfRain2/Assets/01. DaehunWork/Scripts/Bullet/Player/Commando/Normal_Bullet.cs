using RiskOfRain2.Bullet;
using RiskOfRain2.Manager;
using UnityEngine;
namespace RiskOfRain2.Player.Commando
{

	public class Normal_Bullet : Bullet_Base
	{
		public override void OnCollision()
		{
			GameObject tempObject = ObjectPoolManager.Instance.ObjectPoolPop("NormalBullet_Hit");
			tempObject.transform.position = transform.position;
			tempObject.transform.rotation = transform.rotation;
			tempObject.SetActive(true);
			if (rayHit.collider.tag == "Boss")
			{
				GameManager.Instance.BulletOnCollision(rayHit.collider.gameObject, PlayerDefine.PLAYER_MAIN_SKILL_INDEX);
			}
			base.OnCollision();
		}
	}
}
