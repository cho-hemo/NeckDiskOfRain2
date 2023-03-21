using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MonsterFSM : MonoBehaviour
{
    private const string PLAYER = "Player";
    private static GameObject _player;

    public float SqrDetectRange { get { return _detectRange * _detectRange; } }
    public float SqrMaxAttackRange { get { return _maxAttackRange * _maxAttackRange; } }
    public bool IsAnimationEnd { get; private set; } = false;

    private MonsterState currentState;
    private List<MonsterAction> skillList = new List<MonsterAction>();

    private float _detectRange = 200;
    private float _maxAttackRange = 50;

    /// <summary>
    /// 몬스터의 상태를 변경하는 메서드
    /// </summary>
    /// <param name="newState">새로 변경할 상태</param>
    public void ChangeState(MonsterState newState)
    {
		Debug.Log($"{currentState} -> {newState}");

        currentState.Exit();
        IsAnimationEnd = false;

        currentState = newState;
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

    private void Awake()
    {
        if (_player == null)
        {
            _player = GioleFunc.GetRootObj(PLAYER);
        }
    }

    private void Start()
    {
        InitializeState(new MonsterSpawn(this));
    }

    private void Update()
    {
		GioleFunc.Log(currentState);
		currentState.Loop();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, _maxAttackRange);
    }
}