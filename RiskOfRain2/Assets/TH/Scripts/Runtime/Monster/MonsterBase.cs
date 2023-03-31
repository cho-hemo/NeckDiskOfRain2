using UnityEngine;

public enum MonsterType
{
    NORMAL = 0,
    BOSS
}

public class MonsterBase : MonoBehaviour
{
    [SerializeField] protected MonsterData _data;
    protected Animator _anim;

    [field: SerializeField] public MonsterType Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int MaxHp { get; private set; }
    [field: SerializeField] public int Hp { get; protected set; }
    [field: SerializeField] public int Power { get; protected set; }
    [field: SerializeField] public float Speed { get; protected set; }
    [field: SerializeField] public int MaxSqrDetectRange { get; private set; }
    [field: SerializeField] public int MinSqrDetectRange { get; private set; }

    /// <summary>
    /// 몬스터의 데이터를 설정하는 메서드
    /// </summary>
    public virtual void Initialize()
    {
        Name = _data.Name;
        Type = _data.Type;
        Hp = MaxHp = _data.Health;
        Power = _data.Power;
        Speed = _data.Speed;
        MaxSqrDetectRange = _data.MaxSqrDetectRange;
        MinSqrDetectRange = _data.MinSqrDetectRange;
    }

    /// <summary>
    /// 데미지를 받는 메서드
    /// </summary>
    /// <param name="damage"></param>
    public virtual void OnDamaged(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            OnDie();
            return;
        }
    }

    protected virtual void OnDie()
    {
		//GameManager.Instance.ExpEffectSpawn();
    }

    protected virtual void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        Initialize();
    }
}