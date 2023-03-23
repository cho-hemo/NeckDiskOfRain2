using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/Skill")]
public class SkillData : ScriptableObject
{
    [SerializeField] private string _name = "Default";
    public string Name { get { return _name; } }

    [SerializeField] private int _power = 4;
    public int Power { get { return _power; } }

    [SerializeField] private int _range = 100;
    public int Range { get { return _range; } }

    [SerializeField] private int _coolDownTime = 5;
    public int CoolDownTime { get { return _coolDownTime; } }

    [SerializeField] private BoxCollider _rangeCollider;

    public virtual void OnSkill(Monster1 caster)
    {

    }
}