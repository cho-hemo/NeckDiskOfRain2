using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

public class Beetle : Monster
{
    public LayerMask targetLayer; // 추적 대상 레이어
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;

    public Animator beetleAnimator;

    public float speed = default; // 이동 속도
    public float attack = default; // 공격력
    private float timeBetAttack = 0.5f; // 공격 간격
    private float lastAttackTime;  // 마지막 공격 시점

    public float lookRange = default; // 시야 거리

    public string spawnAnime = ""; // 생성 애니메이션
    public string dieAnime = ""; // 사망 애니메이션
    private string moveAnime = "isMove";
    public string attckAnime = "";
    public string idleAnime = "";

    string nowMode = "";

    void Start()
    {
        nowMode = idleAnime;
        navMeshAgent = GetComponent<NavMeshAgent>();
        this.GetComponent<Animator>().Play(spawnAnime);
        //navMeshAgent.updatePosition = false;
        //navMeshAgent.updateRotation = false;

        beetleAnimator.SetBool(moveAnime, true);
    }

    void Update()
    {
        // navMeshAgent.SetDestination(target.position);
        //this.GetComponent<Animator>().Play();
        //if (beetleAnimator.GetBool(moveAnime) == false)
        //{
        //    beetleAnimator.SetBool(moveAnime, true);
        //}

        Debug.Log(beetleAnimator.deltaPosition);
        //Debug.Log(beetleAnimator.hasRootMotion);
        //transform.position = beetleAnimator.rootPosition;
    }



    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRange);
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
