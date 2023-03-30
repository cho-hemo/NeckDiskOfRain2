using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob : MonoBehaviour
{
	public Transform Target;
	NavMeshAgent Pathfinder; // 네비매쉬
	public float LookRange = 20f; // 시야 영역
	public float AttackRange = 4f; // 공격 영역
	public float Speed = default;

	public float atkCooltime = 4;
	public float atkDelay;

	void Start()
    {
		Target = GameObject.FindGameObjectWithTag("Player").transform;
	}

    // Update is called once per frame
    void Update()
    {
        if(atkDelay>=0)
			atkDelay -= Time.deltaTime;
    }

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, LookRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, AttackRange);
	}
}
