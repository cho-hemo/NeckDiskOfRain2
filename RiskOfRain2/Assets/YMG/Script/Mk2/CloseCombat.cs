using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloseCombat : MonoBehaviour
{
	public BoxCollider closeCombat;
	public BeetleMk4 beetle;

	private void Start()
	{
		closeCombat.enabled = false;

	}

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
}
