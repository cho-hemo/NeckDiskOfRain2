using UnityEngine;
using System.Collections.ObjectModel;

public enum MonsterType
{
    NORMAL = 0,
    BOSS
}

public class Monster1 : MonoBehaviour
{
    //
    public MonsterData TestData;

    [field:SerializeField] public MonsterType Type { get; private set; }
    [field:SerializeField] public int MaxHp { get; private set; }
    [field:SerializeField] public int Hp { get; private set; }
    [field:SerializeField] public int Power { get; private set; }
    [field:SerializeField] public int Speed { get; private set; }
    [field:SerializeField] public int skillCount { get; private set; }

    private MonsterData _data;
    private MonsterFSM _fsm;
    private ReadOnlyCollection<SkillData> _skills;

    /// <summary>
    /// 몬스터의 데이터를 설정하는 메서드
    /// </summary>
    /// <param name="data">몬스터 SO 데이터</param>
    public void Initialize(MonsterData data)
    {
        _data = data;
        Type = _data.Type;
        Hp = MaxHp = _data.Health;
        Power = _data.Power;
        Speed = _data.Speed;
        _skills = _data.Skills;
        skillCount = _data.Skills.Count;

        _fsm.Initialize(_skills, new MonsterSpawn(_fsm));
    }

    public void OnSkill()
    {
        SkillData skill = SelectSkill();
        if (skill != null)
        {
            skill.OnSkill(this);
        }
    }

    public void OnDamaged(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            OnDie();
            return;
        }

        if (Type == MonsterType.NORMAL)
        {
            _fsm.ChangeState(new MonsterOnDamaged(_fsm));
        }
    }


    private void Awake()
    {
        _fsm = GetComponent<MonsterFSM>();
    }

    private void Start()
    {
        //
        Initialize(TestData);
    }

    private void Update()
    {

    }

    private SkillData SelectSkill()
    {
        SkillData skill = null;

        return skill;
    }

    private void OnDie()
    {
        _fsm.ChangeState(new MonsterDeath(_fsm));
    }
}