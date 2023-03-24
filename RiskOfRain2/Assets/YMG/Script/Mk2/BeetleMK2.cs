using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeetleMK2 : MonoBehaviour
{
    public float lookRange = 20f; // 시야 영역
    public float attackRange = 4f; // 공격 영역
    public float speed = default; // 이동속도

	private bool isLook = default;

    NavMeshAgent pathfinder; // 네비매쉬
    Transform target; // 목표
    Animator anime; 
	public BoxCollider closeCombat;

    public AnimationClip SpawnAnime;


    void Start()
    {
        //SpawnAnime.GetComponent<Animation>().Play();

        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        anime = GetComponent<Animator>();
        anime.SetBool("isMove", false);
        anime.SetBool("isAttack", false);
		anime.SetBool("isDead", false);

		closeCombat.enabled = false;

		isLook = false;

		//StartCoroutine(UpdatePath()); // 밑의 코루틴 실행
		StartCoroutine(AnimeWaiting());
    }

    IEnumerator AnimeWaiting()
    {
        //Debug.Log($"애니 길이가 몇초? {SpawnAnime.length}");
        yield return new WaitForSeconds(SpawnAnime.length);
        
        // 애니메이션 발동 조건 
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
				isLook = true;
                FaceTarget(); // 적을 바라보고
                pathfinder.SetDestination(target.position); // 적을 향해 간다

                anime.SetBool("isMove", true); // 이동 애니메이션

				//if (distance <= pathfinder.stoppingDistance)
				//{
				//	FaceTarget();
				//	anime.SetBool("isMove", false);
				//}
			}

            if (distance <= attackRange)
            {
				//FaceTarget();
				//pathfinder.SetDestination(transform.position);
				isLook = false;
				anime.SetBool("isMove", false);
				anime.SetBool("isAttack", true);

				pathfinder.SetDestination(target.position);
				//StartCoroutine(Attack());

				if (distance <= pathfinder.stoppingDistance + attackRange)
				{
					pathfinder.SetDestination(transform.position);
					//anime.SetBool("isMove", false);
					//anime.SetBool("isAttack", true);
				}
			}
        }
    }

	//IEnumerator Attack() 
	//{
	//	anime.SetBool("isMove", false);
	//	anime.SetBool("isAttack", true);

	//	yield return new WaitForSeconds(0.3f);
	//	closeCombat.enabled = true;

	//	yield return new WaitForSeconds(1.0f);
	//	closeCombat.enabled = false;

	//	anime.SetBool("isMove", true);
	//	anime.SetBool("isAttack", false);
	//}

    void FaceTarget()
    {
		if (isLook)
		{
			// direction to the target
			Vector3 direction = (target.position - transform.position).normalized;
			// rotation where we point to that target
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			// update our own rotation to point in this direction
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
		}
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

	void DeathCheck()
	{
		pathfinder.enabled = false;
		anime.SetBool("isDead", true);
		gameObject.SetActive(false);
	}

	public void NewEvent()
	{
		Debug.Log("Event");
	}
}
