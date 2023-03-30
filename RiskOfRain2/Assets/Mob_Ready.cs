using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob_Ready : StateMachineBehaviour
{
	Mob mob;
	Transform mobTransform;
	NavMeshAgent Pathfinder;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		mob = animator.GetComponent<Mob>();
		mobTransform = animator.GetComponent<Transform>();
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (mob.atkDelay <= 0)
		{
			animator.SetTrigger("Trigger");
		}

		FaceTarget();

		float distance = Vector3.Distance(mob.Target.position, mobTransform.position);

		if (distance > mob.AttackRange)
		{
			animator.SetBool("isMove", true);
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}

	void FaceTarget()
	{
		Debug.Log("바라보기");
		// direction to the target
		Vector3 direction = (mob.Target.position - mobTransform.position).normalized;
		// rotation where we point to that target
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		// update our own rotation to point in this direction
		mobTransform.rotation = Quaternion.Slerp(mobTransform.rotation, lookRotation, Time.deltaTime * mob.Speed);
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
