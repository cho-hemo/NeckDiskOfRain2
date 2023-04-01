using UnityEngine;
using System.Collections.ObjectModel;
using System.Collections;

public class MonsterFSM : MonoBehaviour
{
    private const string PLAYER = "Player";
    private static GameObject _player;

    public bool IsAnimationEnd { get; private set; } = false;
    public MonsterState CurrentState { get; private set; }

    /// <summary>
    /// 클래스를 초기화하는 메서드
    /// </summary>
    /// <param name="skillList">스킬 정보</param>
    /// <param name="initState">초기 상태</param>
    public void Initialize(ReadOnlyCollection<SkillData> skillList, MonsterState initState)
    {
        InitializeState(initState);
    }

    /// <summary>
    /// 몬스터의 상태를 변경하는 메서드
    /// </summary>
    /// <param name="newState">새로 변경할 상태</param>
    public void ChangeState(MonsterState newState)
    {
        Debug.Log($"{CurrentState} -> {newState}");

        CurrentState.Exit();
        IsAnimationEnd = false;

        CurrentState = newState;
        CurrentState.Enter();
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

    private void InitializeState(MonsterState initState)
    {
        IsAnimationEnd = false;

        CurrentState = initState;
        CurrentState.Enter();
    }

    private void Awake()
    {
        if (_player == null)
        {
            _player = GioleFunc.GetRootObj(PLAYER);
        }
    }

    private void Update()
    {
        CurrentState.Loop();
    }
}