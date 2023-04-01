using UnityEngine;
using RiskOfRain2.Manager;

public enum MonsterType
{
    NORMAL = 0,
    BOSS
}

public class MonsterBase : MonoBehaviour
{
    protected Animator _anim;

    [field: SerializeField] public MonsterType Type { get; protected set; }
    [field: SerializeField] public string Name { get; protected set; }
    [field: SerializeField] public int MaxHp { get; protected set; }
    [field: SerializeField] public int Hp { get; protected set; }
    [field: SerializeField] public int Power { get; protected set; }
    [field: SerializeField] public float Speed { get; protected set; }

    /// <summary>
    /// 몬스터의 데이터를 설정하는 메서드
    /// </summary>
    public virtual void Initialize()
    {

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
        GameManager.Instance.ExpEffectSpawn(Random.Range(3, 5 + 1), transform);
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