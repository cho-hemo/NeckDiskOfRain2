using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemuAttack : MonoBehaviour
{
	public BoxCollider closeCombat;
	public Lemurian lemurian;

	private void Start()
	{
		closeCombat.enabled = false;
	}

	private void BiteOn()
	{
		Debug.Log("Bite on");
		closeCombat.enabled = true;
		lemurian.headAttack = true;
	}

	private void BiteOff()
	{
		Debug.Log("Bite off");
		closeCombat.enabled = false;
		lemurian.headAttack = false;
	}

	private void BreathOn()
	{
		Debug.Log("Breath on");
		lemurian.headAttack = true;
	}

	private void BreathOff()
	{
		Debug.Log("Breath off");
		lemurian.headAttack = false;
	}
}
