using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeetleMK2 : MonoBehaviour
{
    public float LookRange = 20f; // 시야 영역
    public float AttackRange = 4f; // 공격 영역
    public float Speed = default; // 이동속도

	private bool _isLook = default;

    NavMeshAgent Pathfinder; // 네비매쉬
    Transform Target; // 목표
    Animator Anime; 

	//public BoxCollider CloseCombat;
    public AnimationClip SpawnAnime;

	private Vector2 _velocity;
	private Vector2 _smoothDeltaPosition;

	private void Awake()
	{
        Pathfinder = GetComponent<NavMeshAgent>();
        //Anime = GetComponent<Animator>();
		Anime = Pathfinder.GetComponent<Animator>();

		Anime.applyRootMotion = true;
		Pathfinder.updatePosition = false; // 애니메이터가 움직임
		Pathfinder.updateRotation = true;

	}

	private void OnAnimatorMove()
	{
		Vector3 rootPosition = Anime.rootPosition;
		transform.position = rootPosition;
		Pathfinder.nextPosition = rootPosition;
	}

	void Start()
    {
        //SpawnAnime.GetComponent<Animation>().Play();

        Target = GameObject.FindGameObjectWithTag("Player").transform;

        //Anime.SetBool("isMove", false);
        Anime.SetBool("isAttack", false);
		Anime.SetBool("isDead", false);

		//CloseCombat.enabled = false;

		_isLook = false;

		//StartCoroutine(UpdatePath()); // 밑의 코루틴 실행
		StartCoroutine(AnimeWaiting());
    }

    IEnumerator AnimeWaiting()
    {
        //Debug.Log($"애니 길이가 몇초? {SpawnAnime.length}");
        yield return new WaitForSeconds(SpawnAnime.length);
        
        // 애니메이션 발동 조건 
        Anime.SetBool("SpawnEnd", true);
    }

	IEnumerator Term() 
	{
		_isLook = true;
		Pathfinder.SetDestination(transform.position);
		//Anime.SetBool("isMove", false);
		Anime.SetBool("isAttack", true);
		yield return new WaitForSeconds(1.0f);
		_isLook = false;
		//Anime.SetBool("isMove", true);
		Anime.SetBool("isAttack", false);
	}


	void Update()
    {
		SynchronizeAnimatorAndAgent();

		if (Anime.GetBool("SpawnEnd"))
        {
            // target and my distance
            float distance = Vector3.Distance(Target.position, transform.position);

            if (distance <= LookRange) 
            {
				_isLook = true;
                FaceTarget(); // 적을 바라보고
                Pathfinder.SetDestination(Target.position); // 적을 향해 간다

                //Anime.SetBool("isMove", true); // 이동 애니메이션

				//if (distance <= pathfinder.stoppingDistance)
				//{
				//	FaceTarget();
				//	anime.SetBool("isMove", false);
				//}
			}

            if (distance <= AttackRange)
            {
				//FaceTarget();
				//pathfinder.SetDestination(transform.position);
				//_isLook = false;
				//Anime.SetBool("isMove", false);
				//Anime.SetBool("isAttack", true);

				StartCoroutine(Term());

				Pathfinder.SetDestination(Target.position);
				//StartCoroutine(Attack());

				//if (distance <= Pathfinder.stoppingDistance + AttackRange)
				//{
				//	Pathfinder.SetDestination(transform.position);
				//	Anime.SetBool("isMove", false);
				//	Anime.SetBool("isAttack", true);
				//}
			}
        }
    }

	private void SynchronizeAnimatorAndAgent()
	{
		Vector3 worldDeltaPosition = Pathfinder.nextPosition - transform.position;
		worldDeltaPosition.y = 0;

		float dx = Vector3.Dot(transform.right, worldDeltaPosition);
		float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
		Vector2 deltaPosition = new Vector2(dx, dy);

		float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
		_smoothDeltaPosition = Vector2.Lerp(_smoothDeltaPosition, deltaPosition, smooth);

		_velocity = _smoothDeltaPosition / Time.deltaTime;
		if (Pathfinder.remainingDistance <= Pathfinder.stoppingDistance)
		{
			_velocity = Vector2.Lerp(
				Vector2.zero,
				_velocity,
				Pathfinder.remainingDistance / Pathfinder.stoppingDistance);
		}

		bool shouldMove = _velocity.magnitude > 0.5f
			&& Pathfinder.remainingDistance > Pathfinder.stoppingDistance;

		Anime.SetBool("move", shouldMove);
		Anime.SetFloat("locomotion", _velocity.magnitude);

		float deltaMagnitude = worldDeltaPosition.magnitude;
		if (deltaMagnitude > Pathfinder.radius / 2f)
		{
			transform.position = Vector3.Lerp(
				Anime.rootPosition,
				Pathfinder.nextPosition,
				smooth
			);
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
		if (_isLook)
		{
			// direction to the target
			Vector3 direction = (Target.position - transform.position).normalized;
			// rotation where we point to that target
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			// update our own rotation to point in this direction
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * Speed);
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

	void DeathCheck()
	{
		Pathfinder.enabled = false;
		Anime.SetBool("isDead", true);
		gameObject.SetActive(false);
	}

	public void NewEvent()
	{
		Debug.Log("Event");
	}
}
