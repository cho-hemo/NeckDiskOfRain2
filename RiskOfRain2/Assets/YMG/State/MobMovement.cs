using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MobStateMachine
{
	public MobIdle idleState;
	public MobMove movingState;

	private void Awake()
	{
		idleState = new MobIdle(this);
		movingState = new MobMove(this);
	}

	protected override MobBaseState GetInitialState()
	{
		return idleState;
	}
}
