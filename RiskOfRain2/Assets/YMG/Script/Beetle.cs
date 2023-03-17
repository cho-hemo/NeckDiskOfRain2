using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

public class Beetle : Monster
{
    public float lookRange = 20f; // �þ� �Ÿ�
    public float attackRange = 3f; // ���� �Ÿ�
    Transform target;
    NavMeshAgent navMeshAgent;

    public LayerMask targetLayer; // ���� ��� ���̾�
    public LayerMask movementMask;

    public Animator beetleAnimator;

    public float speed = default; // �̵� �ӵ�
    public float attack = default; // ���ݷ�
    private float timeBetAttack = 0.5f; // ���� ����
    private float lastAttackTime;  // ������ ���� ����

    public string spawnAnime = ""; // ���� �ִϸ��̼�
    public string dieAnime = ""; // ��� �ִϸ��̼�
    private string moveAnime = "isMove";
    private string attackAnime = "isAttack";
    public string idleAnime = "";

    string nowMode = "";

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        target = TestPlayerManager.instance.player.transform;
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
                // ��ǥ ����

                // ��ǥ ����
                FaceTarget();
            }

        }
        //Debug.Log($"{distance}");
        if (distance <= attackRange)
        {
            navMeshAgent.SetDestination(target.position);

                //Debug.Log($"��ġ�� / ��Ʋ�� �Ÿ� {distance} ���ߴ� ���� == {navMeshAgent.stoppingDistance}");
            if (distance <= navMeshAgent.stoppingDistance + attackRange)
            {
                Debug.Log("��Ʋ�� ������");
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
