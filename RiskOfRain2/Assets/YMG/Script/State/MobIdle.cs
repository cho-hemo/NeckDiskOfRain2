using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobIdle : MobBaseState
{
	public MobIdle(MobMovement stateMachine) : base("MobIdle", stateMachine) { }

	public override void Enter()
	{
		base.Enter();
	}
	public override void UpdateLogic()
	{
		base.UpdateLogic();

	}
	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
	}
	public override void Exit() 
	{ 
		base.Exit(); 
	}
}
