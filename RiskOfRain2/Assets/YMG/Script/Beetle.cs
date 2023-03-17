using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

public class Beetle : Monster
{
    public float lookRange = 20f; // 시야 거리
    public float attackRange = 3f; // 공격 거리
    Transform target;
    NavMeshAgent navMeshAgent;

    public LayerMask targetLayer; // 추적 대상 레이어
    public LayerMask movementMask;

    public Animator beetleAnimator;

    public float speed = default; // 이동 속도
    public float attack = default; // 공격력
    private float timeBetAttack = 0.5f; // 공격 간격
    private float lastAttackTime;  // 마지막 공격 시점

    public string spawnAnime = ""; // 생성 애니메이션
    public string dieAnime = ""; // 사망 애니메이션
    private string moveAnime = "isMove";
    private string attackAnime = "isAttack";
    public string idleAnime = "";

    string nowMode = "";

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        target = PlayerManager.instance.player.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        //nowMode = idleAnime;
        //this.GetComponent<Animator>().Play(spawnAnime);
        //navMeshAgent.updatePosition = false;
        //navMeshAgent.updateRotation = false;
        //beetleAnimator.SetBool(moveAnime, true);
    }

    void Update()
    {
        //FaceTarget();

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRange)
        {

            navMeshAgent.SetDestination(target.position);

            if (distance <= navMeshAgent.stoppingDistance)
            {
                // 목표 공격

                // 목표 보기
                FaceTarget();
            }

        }
        //Debug.Log($"{distance}");
        if (distance <= attackRange)
        {
            navMeshAgent.SetDestination(target.position);

                //Debug.Log($"박치기 / 비틀의 거리 {distance} 멈추는 지점 == {navMeshAgent.stoppingDistance}");
            if (distance <= navMeshAgent.stoppingDistance + attackRange)
            {
                Debug.Log("비틀이 공격함");
                navMeshAgent.SetDestination(transform.position);
                CloseCombat();
            }
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, movementMask)) 
            {
                Debug.Log("We hit" + hit.collider.name + "" + hit.point);
                MoveToPoint(hit.point);
            }
        }

        //this.GetComponent<Animator>().Play();
        //Debug.Log(beetleAnimator.deltaPosition);
        //Debug.Log(beetleAnimator.hasRootMotion);
        //transform.position = beetleAnimator.rootPosition;
    }

    public void MoveToPoint(Vector3 point) 
    {
        navMeshAgent.SetDestination(point);
        if (beetleAnimator.GetBool(moveAnime) == false)
        {
            beetleAnimator.SetBool(moveAnime, true);
        }
    }

    void FaceTarget() 
    {
        // direction to the target
        Vector3 direction = (target.position - transform.position).normalized;
        // rotation where we point to that target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // update our own rotation to point in this direction
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public override void CloseCombat()
    {
        base.CloseCombat();

        if (beetleAnimator.GetBool(attackAnime) == false)
        {
            beetleAnimator.SetBool(attackAnime, true);
        }
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public override void Die()
    {
        base.CloseCombat();
        this.GetComponent<Animator>().Play(dieAnime);
    }

    //private void OnAnimatorMove()
    //{
    //    if (beetleAnimator != null)
    //    {
    //        transform.position += beetleAnimator.deltaPosition;
    //        //Debug.Log(beetleAnimator.deltaPosition);
    //    }
    //}

}
