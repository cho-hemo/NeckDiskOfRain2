using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "ScriptableObjects/NewSkill")]
public class NewSkill : SkillData
{
    public override void OnSkill(Monster1 caster)
    {
        base.OnSkill(caster);
        Debug.Log("SKill111");
    }
}