using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_AttackState : StateMachineBehaviour
{
	Mob mob;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		mob = animator.GetComponent<Mob>();
		mob.transform.LookAt(mob.Target.position);
		mob.transform.eulerAngles = new Vector3(0, mob.transform.eulerAngles.y, mob.transform.eulerAngles.z);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		mob.atkDelay = mob.atkCooltime;
	}

	// OnStateMove is called right after Animator.OnAnimatorMove()
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that processes and affects root motion
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK()
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that sets up animation IK (inverse kinematics)
	//}
}
