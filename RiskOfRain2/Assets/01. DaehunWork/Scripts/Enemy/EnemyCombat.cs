using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiskOfRain2.Player;

public class EnemyCombat : MonoBehaviour
{
	public float damage;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//int damage_ = normalMonster.Power * 10;
			other.GetComponent<PlayerBase>().TakeDamage(damage);
		}
	}
}
