using UnityEngine;
using System.Collections.ObjectModel;

public class MonsterFSM : MonoBehaviour
{
    private const string PLAYER = "Player";
    private static GameObject _player;

    public float SqrDetectRange { get { return _detectRange * _detectRange; } }
    public float SqrMaxAttackRange { get { return _maxAttackRange * _maxAttackRange; } }
    public bool IsAnimationEnd { get; private set; } = false;

    private MonsterState currentState;

    private float _detectRange = 200;
    private float _maxAttackRange = 50;
    private ReadOnlyCollection<SkillData> _skills;

    /// <summary>
    /// 클래스를 초기화하는 메서드
    /// </summary>
    /// <param name="skillList">스킬 정보</param>
    /// <param name="initState">초기 상태</param>
    public void Initialize(ReadOnlyCollection<SkillData> skillList, MonsterState initState)
    {
        _skills = skillList;

        //스킬 쿨타임 가져오기



        InitializeState(initState);
    }

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
    /// 사용 가능한 스킬을 선택하는 메서드
    /// </summary>
    /// <returns>스킬 사용 가능 여부</returns>
    public bool TrySelectSkill(out int skillNum)
    {
        bool isSelected = false;
        skillNum = 0;

        foreach (SkillData skill in _skills)
        {
            //if (skill.CoolDownTime)
            //{
            //    //
            //}
        }

        return isSelected;
    }

    /// <summary>
    /// 플레이어 바라보는 메서드
    /// </summary>
    public void LookAtPlayer()
    {
        transform.LookAt(_player.transform);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
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

        currentState = initState;
        currentState.Enter();
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
        currentState.Loop();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _maxAttackRange);
    }
}