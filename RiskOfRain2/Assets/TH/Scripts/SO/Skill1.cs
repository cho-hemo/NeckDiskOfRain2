using UnityEngine;

[CreateAssetMenu(fileName = "Skill1", menuName = "ScriptableObjects/Skill1")]
public class Skill1 : SkillData
{
    public override void OnSkill(Monster1 caster)
    {
        base.OnSkill(caster);
        Debug.Log("SKill111");
    }
}

[CreateAssetMenu(fileName = "Skill2", menuName = "ScriptableObjects/Skill2")]
public class Skill2 : SkillData
{
	[SerializeField] private GameObject a;

    public override void OnSkill(Monster1 caster)
    {
        base.OnSkill(caster);
        Debug.Log("SKill222");
    }
}