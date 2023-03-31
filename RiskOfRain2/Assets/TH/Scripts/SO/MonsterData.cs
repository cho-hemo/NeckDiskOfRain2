using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/Monster")]
public class MonsterData : ScriptableObject
{
    [SerializeField] private string _name = "Default";
    public string Name { get { return _name; } }

	[SerializeField] private string _secondName = "Default";
	public string SecondName { get { return _secondName; } }

	[SerializeField] private MonsterType _type = MonsterType.BOSS;
    public MonsterType Type { get { return _type; } }

    [SerializeField] private int _health = 1000;
    public int Health { get { return _health; } }

    [SerializeField] private int _power = 4;
    public int Power { get { return _power; } }

    [SerializeField] private int _speed = 8;
    public int Speed { get { return _speed; } }

    [SerializeField] private int _minSqrDetectRange = 100;
    public int MinSqrDetectRange { get { return _minSqrDetectRange; } }

    [SerializeField] private int _maxSqrDetectRange = 1600;
    public int MaxSqrDetectRange { get { return _maxSqrDetectRange; } }

    [SerializeField] private List<SkillData> skills = new List<SkillData>()
    {

    };
    public ReadOnlyCollection<SkillData> Skills { get { return skills.AsReadOnly(); } }
}