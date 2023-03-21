using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeetleMK2 : MonoBehaviour
{
    public float lookRange = 20f;
    public float attackRange = 3f;
    public float speed = default;



    NavMeshAgent pathfinder;
    Transform target;

    Animator anime;

    public AnimationClip SpawnAnime;

    void Start()
    {
        //SpawnAnime.GetComponent<Animation>().Play();

        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        anime = GetComponentInChildren<Animator>();
        anime.SetBool("isMove", false);
        anime.SetBool("isAttack", false);
        //StartCoroutine(UpdatePath()); // 밑의 코루틴 실행
        StartCoroutine(AnimeWaiting());
    }

    IEnumerator AnimeWaiting()
    {
        //Debug.Log($"애니 길이가 몇초? {SpawnAnime.length}");
        
        yield return new WaitForSeconds(SpawnAnime.length);
        //yield return null;
        anime.SetBool("SpawnEnd", true);
    }

    void Update()
    {
        if (anime.GetBool("SpawnEnd"))
        {
            // target and my distance
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRange)
            {
                //FaceTarget();

                anime.SetBool("isMove", true);

                if (distance <= pathfinder.stoppingDistance)
                {

                    //FaceTarget();
                }
                pathfinder.SetDestination(target.position);
            }

            if (distance <= attackRange)
            {
                pathfinder.SetDestination(target.position);

                if (distance <= pathfinder.stoppingDistance + attackRange)
                {
                    pathfinder.SetDestination(transform.position);
                    anime.SetBool("isMove", false);
                    anime.SetBool("isAttack", true);

                }
            }
        }
    }

    void FaceTarget()
    {
        // direction to the target
        Vector3 direction = (target.position - transform.position).normalized;
        // rotation where we point to that target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // update our own rotation to point in this direction
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }

    //IEnumerator UpdatePath()
    //{
    //    float refreshRate = .25f;

    //    while (target != null)
    //    {
    //        Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
    //        if (!dead)
    //        {
    //            pathfinder.SetDestination(targetPosition);
    //        }
    //        yield return new WaitForSeconds(refreshRate);
    //    }
    //}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
