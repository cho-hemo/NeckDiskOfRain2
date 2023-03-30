using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : AIState
{
	public AIAttack attackState;
	public bool isInAttackRange;

	public override AIState RunCurrentState()
	{
		if (isInAttackRange)
		{
			return attackState;
		}
		else
		{
			return this;
		}
	}
}
