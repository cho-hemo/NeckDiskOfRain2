using UnityEngine;

[CreateAssetMenu(fileName = "Skill_BeetleQueen", menuName = "ScriptableObjects/Skill1")]
public class Skill1 : SkillData
{
    public override void OnSkill(Monster1 caster)
    {
        base.OnSkill(caster);
        Debug.Log("SKill111");
    }
}