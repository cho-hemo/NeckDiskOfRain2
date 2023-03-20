using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class Enemy : LivingThings
{
    NavMeshAgent pathfinder; // 길찾기 관리
    Transform target; // 찾을 목표

    protected override void Start() // 의 Start() 와 겹침
    {
        base.Start();
        pathfinder= GetComponent<NavMeshAgent>(); // 할당
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // 플레이어는 "Player" 태그를 가지고 있다

        StartCoroutine(UpdatePath()); // 밑의 코루틴 실행
    }

    void Update()
    {
        //pathfinder.SetDestination(target.position);
        //// 목적지 타겟 위치
    }

    IEnumerator UpdatePath() // 코루틴
    { // 매 프레임마다 업데이트 하지 말고, 타이머로 고정된 간격
        float refreshRate = .25f;

        while (target != null) 
        { // 타겟이 존재하는 동안
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            if (!dead)
            {
                pathfinder.SetDestination(targetPosition);
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
