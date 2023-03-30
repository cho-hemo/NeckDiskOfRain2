using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/Skill")]
public class SkillData : ScriptableObject
{
    [SerializeField] private string _name = "Default";
    public string Name { get { return _name; } }

    [SerializeField] private int _power = 1;
    public int Power { get { return _power; } }

    [SerializeField] private int _sqrRange = 490;
    public int SqrRange { get { return _sqrRange; } }

    [SerializeField] private int _coolDownTime = 5;
    public int CoolDownTime { get { return _coolDownTime; } }

    //[SerializeField] private BoxCollider _rangeCollider;
}
