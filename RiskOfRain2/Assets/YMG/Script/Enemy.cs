using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingThings
{
	NavMeshAgent pathfinder; // ��ã�� ����
	Transform target; // ã�� ��ǥ

	protected override void Start() // �� Start() �� ��ħ
	{
		base.Start();
		pathfinder = GetComponent<NavMeshAgent>(); // �Ҵ�
		target = GameObject.FindGameObjectWithTag("Player").transform;
		// �÷��̾�� "Player" �±׸� ������ �ִ�

		StartCoroutine(UpdatePath()); // ���� �ڷ�ƾ ����
	}

	void Update()
	{
		//pathfinder.SetDestination(target.position);
		//// ������ Ÿ�� ��ġ
	}

	IEnumerator UpdatePath()
	{
		float refreshRate = 1f;

		while (target != null)
		{
			Vector3 targetPosition = new Vector3(target.position.x, target.position.z);
			pathfinder.SetDestination(targetPosition);
			yield return new WaitForSeconds(refreshRate);
		}
	}
}
