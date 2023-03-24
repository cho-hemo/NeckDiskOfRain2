using UnityEngine;
using UnityEngine.AI;

public class TestBeetle : MonoBehaviour
{
	Vector3 worldDeltaPosition = Vector3.zero;
	Vector3 position = Vector3.zero;
	NavMeshAgent agent;
	Transform Target; // 목표
	Animator animator;

	void Start()
	{
		Target = GameObject.FindGameObjectWithTag("Player").transform;

		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		// don't update the agent's position, the animation will do that
		agent.updatePosition = false;
	}

	private void Update()
	{
		worldDeltaPosition = agent.nextPosition - transform.position;
		agent.SetDestination(Target.position); // 적을 향해 간다

		// Pull agent towards character
		if (worldDeltaPosition.magnitude > agent.radius)
			agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;
	}

	private void OnAnimatorMove()
	{
		// Update position based on animation movement using navigation surface height
		position = animator.rootPosition;
		position.y = agent.nextPosition.y;
		transform.position = position;
	}
}
