using System.Collections.ObjectModel;
using UnityEngine;

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
    [SerializeField] public ReadOnlyCollection<SkillData> Skills { get; private set; }

    private MonsterData _data;
    private MonsterFSM _fsm;

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
        Skills = _data.Skills;

        _fsm.InitializeState(new MonsterSpawn(_fsm));
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




	[SerializeField] private GameObject spit;
	[SerializeField] private Transform spitStartPos;
	[SerializeField] private int spitCount = 6;
	[SerializeField] private int degree = 150;

	public void FireSpit()
	{
		for (int i = 0; i < spitCount; i++)
		{
			GameObject inst = Instantiate(spit, new Vector3 (spitStartPos.position.x, spitStartPos.position.y, spitStartPos.position.z), 
				Quaternion.Euler(-30, -(degree / 2) - (degree / 5) * i, 0));
		}
	}
}