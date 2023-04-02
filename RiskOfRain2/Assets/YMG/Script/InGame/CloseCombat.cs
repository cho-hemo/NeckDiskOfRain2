using RiskOfRain2;
using RiskOfRain2.Manager;
using RiskOfRain2.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloseCombat : MonoBehaviour
{
	public int damage = 10;

	public BoxCollider closeCombat;
	public BeetleMk4 beetle;
	//public NormalMonsterBase normalMonster;

	private void HeadAttackOn()
	{
		Debug.Log("Head attack on");
		closeCombat.enabled = true;
		beetle.headAttack = true;
	}

	private void HeadAttackOff()
	{
		Debug.Log("Head attack off");
		closeCombat.enabled = false;
		beetle.headAttack = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//int damage_ = normalMonster.Power * 10;
			other.GetComponent<PlayerBase>().TakeDamage(damage);
			closeCombat.enabled = false;
		}
	}
}
