using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : AIState
{
	public override AIState RunCurrentState()
	{
		Debug.Log("공격");
		return this;
	}
}
