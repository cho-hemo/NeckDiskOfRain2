using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdle : AIState
{
	public AIChase chaseState;
	public bool canSeeThePlayer;

	public override AIState RunCurrentState()
	{
		if (canSeeThePlayer)
		{
			return chaseState;
		}
		else 
		{ 
			return this;
		}
	}
}
