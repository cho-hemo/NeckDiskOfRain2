using RiskOfRain2.Bullet;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RiskOfRain2.Manager;
namespace RiskOfRain2.Player.Commando
{
	public class Phase_Round_Bullet : Bullet_Base
	{
		public override void OnCollision()
		{
			GameObject tempObject = ObjectPoolManager.Instance.ObjectPoolPop("Phase_Round_Hit");
			tempObject.transform.position = transform.position;
			tempObject.transform.rotation = transform.rotation;
			tempObject.SetActive(true);
			base.OnCollision();
		}
	}
}

