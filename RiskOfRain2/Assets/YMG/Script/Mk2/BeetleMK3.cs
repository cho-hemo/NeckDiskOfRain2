using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeetleMK3 : MonoBehaviour
{
	private Animator animator;
	private NavMeshAgent agent;

	private const string isMove = "isMove";

    void Start()
    {
        animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();

		agent.updatePosition = false;
		agent.updateRotation = true;
    }

    void Update()
    {
        agent.nextPosition = transform.position;
    }

	public void Walk() 
	{
		animator.SetBool(isMove, true);
	}
	public void Idle()
	{
		animator.SetBool(isMove, false);
	}
}
