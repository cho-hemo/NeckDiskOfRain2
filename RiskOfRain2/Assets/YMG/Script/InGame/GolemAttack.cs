using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttack : MonoBehaviour
{
	public BoxCollider closeCombat;
	public Golem golem;

	private void Start()
	{
		closeCombat.enabled = false;
	}

	private void SmackOn()
	{
		Debug.Log("Smack on");
		closeCombat.enabled = true;
		golem.headAttack = true;
	}

	private void SmackOff()
	{
		Debug.Log("Smack off");
		closeCombat.enabled = false;
		golem.headAttack = false;
	}

	private void LaserOn()
	{
		Debug.Log("Laser on");
		golem.headAttack = true;
	}

	private void LaserOff()
	{
		Debug.Log("Laser off");
		golem.headAttack = false;
	}
}
