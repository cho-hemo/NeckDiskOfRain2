using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill2", menuName = "ScriptableObjects/Skill2")]
public class Skill2 : SkillData
{
    [SerializeField] private GameObject spit;
    [SerializeField] private int spitCount = 6;
    [SerializeField] private int degree = 150;

    public override void OnSkill(Monster1 caster)
    {
        base.OnSkill(caster);
        for (int i = 0; i < spitCount; i++)
        {
            GameObject inst = Instantiate(spit, caster.transform.position, Quaternion.Euler(0, -degree / 2 + degree / 6, 0));
        }
    }
}