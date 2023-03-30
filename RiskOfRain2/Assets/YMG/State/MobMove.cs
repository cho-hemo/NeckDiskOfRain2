using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMove : MobBaseState
{
	public MobMove(MobMovement stateMachine) : base("MobMove", stateMachine) { }

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
