using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class Enemy : LivingThings
{
    NavMeshAgent pathfinder; // ��ã�� ����
    Transform target; // ã�� ��ǥ

    protected override void Start() // �� Start() �� ��ħ
    {
<<<<<<< HEAD
        pathfinder = GetComponent<NavMeshAgent>();
=======
        base.Start();
        pathfinder= GetComponent<NavMeshAgent>(); // �Ҵ�
>>>>>>> f04aa8b4a60290ce456c87dc99869efb5253e9a5
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // �÷��̾�� "Player" �±׸� ������ �ִ�

        StartCoroutine(UpdatePath()); // ���� �ڷ�ƾ ����
    }

    void Update()
    {
        //pathfinder.SetDestination(target.position);
        //// ������ Ÿ�� ��ġ
    }

<<<<<<< HEAD
    IEnumerator UpdatePath()
    {
        float refreshRate = 1f;

        while (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.z);
            pathfinder.SetDestination(targetPosition);
=======
    IEnumerator UpdatePath() // �ڷ�ƾ
    { // �� �����Ӹ��� ������Ʈ ���� ����, Ÿ�̸ӷ� ������ ����
        float refreshRate = .25f;

        while (target != null) 
        { // Ÿ���� �����ϴ� ����
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            if (!dead)
            {
                pathfinder.SetDestination(targetPosition);
            }
>>>>>>> f04aa8b4a60290ce456c87dc99869efb5253e9a5
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
