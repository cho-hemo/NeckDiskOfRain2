using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MonsterFSM : MonoBehaviour
{
    public float SqrDetectRange { get { return _detectRange * _detectRange; } }
    public float SqrMaxAttackRange { get { return _maxAttackRange * _maxAttackRange; } }
    public bool IsAnimationEnd { get; private set; } = false;

    private static GameObject _player;

    private MonsterState currentState;
    private List<MonsterAction> skillList = new List<MonsterAction>();

    private float _detectRange = 0;
    private float _maxAttackRange = 0;


	/// <summary>
	/// 몬스터의 상태를 변경하는 메서드
	/// </summary>
	/// <param name="newState">새로 변경할 상태</param>
    public void ChangeState(MonsterState newState)
    {
        IsAnimationEnd = false;
        currentState = newState;

        currentState.Exit(); 
        currentState.Enter();
    }

	/// <summary>
	/// 몬스터에서 플레이어까지의 거리의 제곱을 구하는 메서드
	/// </summary>
	/// <returns>플레이어까지의 거리의 제곱</returns>
    public float GetSqrDistanceToPlayer()
    {
        return Functions.GetSqrDistance(transform.position, _player.transform.position);
    }

	/// <summary>
	/// 애니메이션 종료 시 호출되는 메서드
	/// </summary>
	public void OnAnimationExit()
	{
		IsAnimationEnd = true;
	}

	private void InitializeState(MonsterState defaultState)
    {
        IsAnimationEnd = false;
        currentState = defaultState;

        currentState.Enter();
    }

    private void Start()
    {
        InitializeState(new MonsterSpawn(this));
    }

    private void Update()
    {
        currentState.Loop();

        //switch (State)
        //{
        //	case MonsterStateType.IDLE:
        //		distanceToPlayer = (player.transform.position - transform.position).sqrMagnitude;
        //		if (distanceToPlayer <= sqrDetectRange)
        //		{
        //			ChangeState(new MonsterMove());
        //		}
        //		break;
        //	case MonsterStateType.MOVE:
        //		if (skillList.Count > 0 && distanceToPlayer <= maxAttackRange)
        //		{
        //			ChangeState(new MonsterAttack());
        //		}
        //		else
        //		{

        //		}
        //		break;
        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere (transform.position, _maxAttackRange);
    }
}