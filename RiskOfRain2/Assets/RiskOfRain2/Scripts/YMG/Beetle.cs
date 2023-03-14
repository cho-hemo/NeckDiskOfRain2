using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

public class Beetle : Monster
{
    public LayerMask targetLayer; // ���� ��� ���̾�
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;

    public Animator beetleAnimator;

    public float speed = default; // �̵� �ӵ�
    public float attack = default; // ���ݷ�
    private float timeBetAttack = 0.5f; // ���� ����
    private float lastAttackTime;  // ������ ���� ����

    public float lookRange = default; // �þ� �Ÿ�

    public string spawnAnime = ""; // ���� �ִϸ��̼�
    public string dieAnime = ""; // ��� �ִϸ��̼�
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
